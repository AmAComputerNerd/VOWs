using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using VOWsLauncher.Events;
using VOWsLauncher.MVVM.Model;
using VOWsLauncher.MVVM.ViewModel.SubWindows;
using VOWsLauncher.SubWindows;

namespace VOWsLauncher.MVVM.ViewModel
{
    public class NewViewModel : ObservableRecipient
    {
        /// <summary>
        /// The <c>Globals</c> property refers to an object full of shared variables across multiple classes.
        /// These may be read-only, or be able to be edited across classes. The <c>Globals</c> class is an <c>ObservableObject</c>.
        /// </summary>
        public Globals Globals { get => Globals.Default; }

        /// <summary>
        /// The <c>NewDocumentWindowCommand</c> command will open an instance of the <c>NewDocumentWindow</c> for user input.
        /// When the 'Done' button is pressed, the editor process will be called with the arguments entered in the fields of these windows.
        /// This application will then exit.
        /// </summary>
        public RelayCommand NewDocumentWindowCommand { get; }
        /// <summary>
        /// The <c>NewWorkspaceWindowCommand</c> command will open an instance of the <c>NewWorkspaceWindow</c> for user input.
        /// When the 'Done' button is pressed, the editor process will be called with the arguments entered in the fields of these windows.
        /// This application will then exit.
        /// </summary>
        public RelayCommand NewWorkspaceWindowCommand { get; }

        /// <summary>
        /// A constructor for <c>NewViewModel</c> that assigns default values to all class variables.
        /// </summary>
        public NewViewModel() 
        {
            // Activate NewViewModel to receive messages.
            IsActive = true;

            // Assign function for the NewDocumentWindowCommand.
            NewDocumentWindowCommand = new(() =>
            {
                // Get an instance of the MainWindow.
                MainWindow mw = (MainWindow)Application.Current.MainWindow;
                // Create an instance of the NewDocumentWindow and set its owner to be MainWindow and a new datacontext.
                Window w = new NewDocumentWindow
                {
                    Owner = mw,
                };
                NewDocumentWindowViewModel nvm = new(w);
                w.DataContext = nvm;
                // Show the NewDocumentWindow as a dialogue, meaning it will halt usage of the main program until the user closes it by clicking one of the buttons.
                if (w.ShowDialog() == true)
                {
                    try
                    {
                        // Success! First, let's create the new file.
                        // Temporary directory check - we must ensure that this directory exists.
                        if (nvm.Directory == null) throw new NullReferenceException("Host directory for document does not exist!");
                        File.Create(nvm.RawDirectory + "/" + ValidateName(nvm.DocumentName) + ".vdoc");
                        // Now that this file exists, we can call the VOWs Editor with arguments -d {path}.
                        Process.Start(new ProcessStartInfo("./VOWs.exe", "-d \"" + Path.Combine(nvm.RawDirectory, ValidateName(nvm.DocumentName) + ".vdoc") + "\""));
                        Application.Current.Shutdown();
                    } 
                    catch(Exception e)
                    {
                        // Something went wrong! We'll notify the user with a small dialogue window.
#pragma warning disable CS8604 // Possible null reference argument.
                        Messenger.Send(new LogMessage(e.Message, ToString(), LogLevel.ERROR));
#pragma warning restore CS8604 // Possible null reference argument.
                        MessageBox.Show("An error occurred in the process of creating a new document!\nPlease read the 'launcher.txt' log for more information!", ValidateName(nvm.DocumentName) + " - Failed");
                    }
                }
            });
            // Assign function for the NewWorkspaceWindowCommand.
            NewWorkspaceWindowCommand = new(() =>
            {
                // Get an instance of the MainWindow.
                MainWindow mw = (MainWindow)Application.Current.MainWindow;
                // Create an instance of the NewWorkspaceWindow and set its owner to be MainWindow and a new datacontext.
                Window w = new NewWorkspaceWindow
                {
                    Owner = mw,
                };
                NewWorkspaceWindowViewModel nvm = new(w);
                w.DataContext = nvm;
                // Show the NewWorkspaceWindow as a dialogue, meaning it will halt usage of the main program until the user closes it by clicking one of the buttons
                if (w.ShowDialog() == true)
                {
                    try
                    {
                        // Success! First, let's create the new file.
                        // Temporary directory check - we must ensure that this directory exists.
                        if (nvm.Directory == null) throw new NullReferenceException("Host directory for workspace does not exist!");
                        File.Create(nvm.RawDirectory + "/" + ValidateName(nvm.WorkspaceName) + ".vwsp");
                        // Now that this file exists, we can call the VOWs Editor with arguments -d {path}.
                        Process.Start(new ProcessStartInfo("./VOWs.exe", "-w \"" + Path.Combine(nvm.RawDirectory, ValidateName(nvm.WorkspaceName) + ".vwsp") + "\""));
                        Application.Current.Shutdown();
                    }
                    catch (Exception e)
                    {
                        // Something went wrong! We'll notify the user with a small dialogue window.
#pragma warning disable CS8604 // Possible null reference argument.
                        Messenger.Send(new LogMessage(e.Message, ToString(), LogLevel.ERROR));
#pragma warning restore CS8604 // Possible null reference argument.
                        MessageBox.Show("An error occurred in the process of creating a new workspace!\nPlease read the 'launcher.txt' log for more information!", ValidateName(nvm.WorkspaceName) + " - Failed");
                    }
                }
            });
        }
        
        /// <summary>
        /// The overriden <c>OnActivated</c> method registers the class with a variety of messagers from the Messenger object.
        /// </summary>
        protected override void OnActivated()
        {
            Messenger.Register<NewViewModel, DisplayNewSubmenuMessage>(this, (r, m) => r.Receive(m));
        }

        /// <summary>
        /// The <c>Receive</c> method will be called whenever the <c>DisplayNewSubmenuMessage</c> is received.
        /// </summary>
        /// <param name="message">The event that was sent, with data.</param>
        private void Receive(DisplayNewSubmenuMessage message)
        {
            if (message.IsWorkspace) NewWorkspaceWindowCommand.Execute(null);
            else NewDocumentWindowCommand.Execute(null);
        }

        /// <summary>
        /// The <c>ValidateName</c> method will validate a file name so that it complies with Windows File Subsystem rules.
        /// This involves removing prefixed and suffixed whitespace and invalid symbols.
        /// </summary>
        /// <param name="name">The file name to validate.</param>
        /// <returns>A validated file name.</returns>
        private static string ValidateName(string name)
        {
            name = name.Trim();
            name = name.Replace("/", "");
            name = name.Replace("\\", "");
            name = name.Replace(":", "");
            name = name.Replace("*", "");
            name = name.Replace("?", "");
            name = name.Replace("\"", "");
            name = name.Replace("<", "");
            name = name.Replace(">", "");
            name = name.Replace("|", "");
            return name;
        }
    }
}
