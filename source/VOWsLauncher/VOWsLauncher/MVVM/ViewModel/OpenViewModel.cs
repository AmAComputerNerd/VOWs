using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using VOWsLauncher.Events;
using VOWsLauncher.MVVM.Model;

namespace VOWsLauncher.MVVM.ViewModel
{
    public class OpenViewModel : ObservableRecipient
    {
        /// <summary>
        /// The <c>Globals</c> property refers to an object full of shared variables across multiple classes.
        /// These may be read-only, or be able to be edited across classes. The <c>Globals</c> class is an <c>ObservableObject</c>.
        /// </summary>
        public Globals Globals { get => Globals.Default; }

        /// <summary>
        /// The <c>OpenDocumentWindowCommand</c> command will open an instance of the <c>OpenFileDialog</c> for user input.
        /// When the 'Done' button is pressed, the editor process will be called with the arguments entered in the field of this window.
        /// This application will then exit.
        /// </summary>
        public RelayCommand OpenDocumentWindowCommand { get; }
        /// <summary>
        /// The <c>OpenWorkspaceWindowCommand</c> command will open an instance of the <c>OpenFileDialog</c> for user input.
        /// When the 'Done' button is pressed, the editor process will be called with the arguments entered in the field of this window.
        /// This application will then exit.
        /// </summary>
        public RelayCommand OpenWorkspaceWindowCommand { get; }

        /// <summary>
        /// A constructor for <c>OpenViewModel</c> that assigns default values to all class variables.
        /// </summary>
        public OpenViewModel()
        {
            // Activate OpenViewModel to receive messages.
            IsActive = true;

            // Assign function for the OpenDocumentWindowCommand.
            OpenDocumentWindowCommand = new(() =>
            {
                // Open the dialogue to retrieve a file.
                string? file = RetrieveFile("VOWs Document files (*.vdoc)|*.vdoc|All files (*.*)|*.*");
                if (file == null) return;
                // Open the VOWs editor.
                try
                {
                    if (file.EndsWith(".vdoc"))
                    {
                        Process.Start(new ProcessStartInfo("./VOWs.exe", "-d \"" + file + "\""));
                    }
                    else if (file.EndsWith(".rtf") || file.EndsWith(".txt"))
                    {
                        Process.Start(new ProcessStartInfo("./VOWs.exe", "-d \"" + file + "\" -legacy true"));
                    }
                    else if (file.EndsWith(".pdf"))
                    {
                        Process.Start(new ProcessStartInfo("./VOWs.exe", "-d \"" + file + "\" -legacy true -readonly true"));
                    }
                    else
                    {
                        // Whatever was selected wasn't valid.
                        Messenger.Send(new LogMessage("Failed to open file '" + file + "' in operation 'open-document': '" + new FileInfo(file).Extension + "' is an unsupported file type.", ToString(), LogLevel.WARNING));
                        MessageBox.Show("The file you selected is not currently supported!\nSupported file extensions for a document include [.vdoc, .rtf, .txt, .pdf].", "Open - " + file);
                        return;
                    }
                    // Close the application.
                    Application.Current.Shutdown();
                } 
                catch (Exception e)
                {
                    // Something went wrong! We'll notify the user with a small dialogue window.
                    Messenger.Send(new LogMessage(e.Message, ToString(), LogLevel.ERROR));
                    MessageBox.Show("An error occurred in the process of opening the document!\nPlease read the 'launcher.txt' log for more information!", "Open (" + file + ") - Failed");
                }
            });
            // Assign function for the OpenWorkspaceWindowCommand.
            OpenWorkspaceWindowCommand = new(() =>
            {
                // Open the dialogue to retrieve a file.
                string? file = RetrieveFile("VOWs Workspace files (*.vwsp)|*.vwsp|All files (*.*)|*.*");
                if (file == null) return;
                try
                {
                    // Open the VOWs editor.
                    if (file.EndsWith(".vwsp") || file.EndsWith(".zip"))
                    {
                        Process.Start(new ProcessStartInfo("./VOWs.exe", "-w \"" + file + "\""));
                    }
                    else
                    {
                        // Whatever was selected wasn't valid.
                        Messenger.Send(new LogMessage("Failed to open file '" + file + "' in operation 'open-workspace': '" + new FileInfo(file).Extension + "' is an unsupported file type.", ToString(), LogLevel.WARNING));
                        MessageBox.Show("The file you selected is not currently supported!\nSupported file extensions for a workspace include [.vwsp, .zip].", "Open - " + file);
                        return;
                    }
                    // Close the application.
                    Application.Current.Shutdown();
                }
                catch (Exception e)
                {
                    // Something went wrong! We'll notify the user with a small dialogue window.
                    Messenger.Send(new LogMessage(e.Message, ToString(), LogLevel.ERROR));
                    MessageBox.Show("An error occurred in the process of opening the workspace!\nPlease read the 'launcher.txt' log for more information!", "Open (" + file + ") - Failed");
                }
            });
        }

        /// <summary>
        /// The overriden <c>OnActivated</c> method registers the class with a variety of messagers from the Messenger object.
        /// </summary>
        protected override void OnActivated()
        {
            Messenger.Register<OpenViewModel, DisplayOpenSubmenuMessage>(this, (r, m) => r.Reply(m));
        }

        /// <summary>
        /// The <c>Reply</c> method will be called whenever the <c>DisplayOpenSubmenuMessage</c> is received.
        /// </summary>
        /// <param name="message">The event to reply to, with data.</param>
        private void Reply(DisplayOpenSubmenuMessage message)
        {
            if (message.Target == TargetType.DOCUMENT) message.Reply(RetrieveFile("VOWs Document files (*.vdoc)|*.vdoc|All files (*.*)|*.*"));
            else if (message.Target == TargetType.WORKSPACE) message.Reply(RetrieveFile("VOWs Workspace files (*.vwsp)|*.vwsp|All files (*.*)|*.*"));
            else message.Reply(RetrieveDirectory());
        }

        /// <summary>
        /// The <c>RetrieveFile</c> method will open a new <c>OpenFileDialog</c> with a specified filter and return the selected file.
        /// If the user cancels without selecting a file, <c>null</c> will be returned.
        /// </summary>
        /// <param name="filter">The filter for the <c>OpenFileDialog</c>.</param>
        /// <returns>The path to the selected file, or <c>null</c> if the user cancelled.</returns>
        private string? RetrieveFile(string filter)
        {
            OpenFileDialog dialog = new()
            {
                Filter = filter
            };
            if(dialog.ShowDialog() == true)
            {
                Messenger.Send(new LogMessage("User finished 'browse file' operation with filter '" + filter + "': '" + dialog.FileName + "'."));
                return dialog.FileName;
            }
            Messenger.Send(new LogMessage("User cancelled 'browse file' operation with filter '" + filter + "'.", ToString(), LogLevel.DEBUG));
            return null;
        }

        /// <summary>
        /// The <c>RetrieveDirectory</c> method will open a new <c>FolderBrowserDialog</c> and return the selected directory.
        /// If the user cancels without selecting a directory, <c>null</c> will be returned.
        /// </summary>
        /// <returns>The path to the selected directory, or <c>null</c> if the user cancelled.</returns>
        private string? RetrieveDirectory()
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new()
            {
                ShowNewFolderButton = true
            };
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Messenger.Send(new LogMessage("User finished 'browse file' operation: '" + dialog.SelectedPath + "'.", ToString(), LogLevel.DEBUG));
                return dialog.SelectedPath;
            }
            Messenger.Send(new LogMessage("User cancelled 'browse directory' operation.", ToString(), LogLevel.DEBUG));
            return null;
        }
    }
}
