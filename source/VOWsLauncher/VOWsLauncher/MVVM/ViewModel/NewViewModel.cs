using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Nodes;
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
        /// The <c>MoreTemplatesCommand</c> command will switch the view to TemplatesView.
        /// </summary>
        public RelayCommand MoreTemplatesCommand { get; }
        /// <summary>
        /// The <c>VOWsTutorial1Command</c> command will select the 'VOWs for Beginners' template before
        /// calling the <c>NewDocumentWindowCommand</c> command.
        /// </summary>
        public RelayCommand VOWsTutorial1Command { get; }
        /// <summary>
        /// The <c>VOWsTutorial2Command</c> command will select the 'VOWs Workspace Tutorial' template
        /// before calling the <c>NewWorkspaceWindowCommand</c> command.
        /// </summary>
        public RelayCommand VOWsTutorial2Command { get; }

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
                        string path = CreateVOWsDirectoryStructure(false, nvm.DocumentName, nvm.RawDirectory, nvm.Template);
                        // Now we can zip the new file, change it's extension and finally copy it to the new location.
                        ZipFile.CreateFromDirectory(path, nvm.RawDirectory + "\\zipped.zip");
                        File.Move(nvm.RawDirectory + "\\zipped.zip", nvm.RawDirectory + "\\" + ValidateName(nvm.DocumentName) + ".vdoc");
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
                        string path = CreateVOWsDirectoryStructure(true, nvm.WorkspaceName, nvm.RawDirectory, nvm.Template);
                        // Now we can zip the new file, change it's extension and finally copy it to the new location.
                        ZipFile.CreateFromDirectory(path, nvm.RawDirectory + "\\zipped.zip");
                        File.Move(nvm.RawDirectory + "\\zipped.zip", nvm.RawDirectory + "\\" + ValidateName(nvm.WorkspaceName) + ".vwsp");
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
                        MessageBox.Show("An error occurred in the process of creating a new workspace!\nPlease read the 'launcher.txt' log for more information!\n" + e.Message, ValidateName(nvm.WorkspaceName) + " - Failed");
                    }
                }
            });
            // Assign function for the MoreTemplatesCommand.
            MoreTemplatesCommand = new(() =>
            {
                // Change the current view to the Templates view.
                WeakReferenceMessenger.Default.Send(new ChangeViewMessage(6));
            });
            // Assign function for the VOWsTutorial1Command.
            VOWsTutorial1Command = new(() =>
            {
                if (!Uri.TryCreate(Environment.CurrentDirectory + "/templates/tutorial1.vdoc", new(), out Uri uri))
                {
                    // The tutorial was deleted or otherwise isn't present.
                    Messenger.Send(new LogMessage("Failed to locate 'templates/tutorial1.vdoc' - aborting template selection.", ToString(), LogLevel.WARNING));
                    MessageBox.Show("Failed to locate 'templates/tutorial1.vdoc' - is the file missing?\nCouldn't select template - please try again.", "Error");
                    return;
                }
                // Template found, so we'll change the selected template before executing NewDocumentWindowCommand.
                Messenger.Send(new ChangeSelectedTemplateMessage(new Template("VOWs for Beginners", "A tutorial aiming to introduce new users to VOWs!", uri, false)));
                NewDocumentWindowCommand.Execute(null);
            });
            // Assign function for the VOWsTutorial2Command.
            VOWsTutorial2Command = new(() =>
            {
                if (!Uri.TryCreate(Environment.CurrentDirectory + "/templates/tutorial2.vwsp", new(), out Uri uri))
                {
                    // The tutorial was deleted or otherwise isn't present.
                    Messenger.Send(new LogMessage("Failed to locate 'templates/tutorial2.vwsp' - aborting template selection.", ToString(), LogLevel.WARNING));
                    MessageBox.Show("Failed to locate 'templates/tutorial2.vwsp' - is the file missing?\nCouldn't select template - please try again.", "Error");
                    return;
                }
                // Template found, so we'll change the selected template before executing NewWorkspaceWindowCommand.
                Messenger.Send(new ChangeSelectedTemplateMessage(new Template("VOWs Workspace Tutorial", "A tutorial aiming to introduce new users to VOWs workspaces!", uri, true)));
                NewWorkspaceWindowCommand.Execute(null);
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

        public static string CreateVOWsDirectoryStructure(bool isWorkspace, string objectName, string rawDirectory, Template template)
        {
            // Define path to /temp/ folder for all unzipping and rezipping operations.
            string path = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName + "\\temp";
            // Create the directory anew if it doesn't exist, or delete it's contents if it does.
            if (Directory.Exists(path))
            {
                DirectoryInfo di = new(path);
                foreach (FileInfo file in di.GetFiles()) file.Delete();
                foreach (DirectoryInfo dir in di.GetDirectories()) dir.Delete(true);
            }
            else Directory.CreateDirectory(path);
            // Now we can begin constructing a new VOWs object in this directory!
            // First, we'll create the 'info.json' file containing information on the file.
            JsonObject infoData = new()
            {
                ["name"] = objectName,
                ["isWorkspace"] = isWorkspace,
                ["path"] = rawDirectory + "/" + ValidateName(objectName) + (isWorkspace ? ".vwsp" : ".vdoc"),
                ["template"] = new JsonObject()
                {
                    ["name"] = template.Equals(Template.Empty) ? "" : template.Name,
                    ["description"] = template.Equals(Template.Empty) ? "" : template.Description,
                    ["path"] = template.Location?.AbsolutePath ?? ""
                }
            };
            // We've now got an info file, so we'll place that in the directory using a JsonWriter.
            using FileStream infoStream = File.Create(path + "\\info.json");
            using Utf8JsonWriter infoWriter = new(infoStream, new() { Indented = true });
            infoData.WriteTo(infoWriter, new() { WriteIndented = true });
            // Next, we'll create a new RTF file at the location.
            // If we're using a workspace, we should create another directory inside to house the file.
            string rtfPath;
            if (isWorkspace)
            {
                rtfPath = path + "\\documents";
                Directory.CreateDirectory(rtfPath);
            }
            else rtfPath = path;
            FileStream rtfStream = File.Create(rtfPath + "\\document.rtf");
            rtfStream.Dispose();
            rtfStream.Close();
            // We'll also create the unused version control file, just for viewing sake.
            JsonObject vcData = new()
            {
                ["1"] = new JsonObject()
                {
                    ["active"] = true,
                    ["type"] = "main"
                }
            };
            using FileStream vcStream = File.Create(path + "\\versions.json");
            using Utf8JsonWriter vcWriter = new(vcStream, new() { Indented = true });
            vcData.WriteTo(vcWriter, new() { WriteIndented = true });
            // Return.
            return path;
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
