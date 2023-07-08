using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.IO;
using System.Security.AccessControl;
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
        /// When the 'Done' button is pressed, an <c>OpenEditorMessage</c> will be broadcast and received by the <c>MainViewModel</c>, which will in turn run the 
        /// <b>VOWs.exe</b> process. This application will then exit.
        /// </summary>
        public RelayCommand NewDocumentWindowCommand { get; }
        /// <summary>
        /// The <c>NewWorkspaceWindowCommand</c> command will open an instance of the <c>NewWorkspaceWindow</c> for user input.
        /// When the 'Done' button is pressed, an <c>OpenEditorMessage</c> will be broadcast and received by the <c>MainViewModel</c>, which will in turn run the 
        /// <b>VOWs.exe</b> process. This application will then exit.
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
                    // Success! First, let's create the new file.
                    // Temporary directory check.
                    if (nvm.Directory == null) return;
                    File.Create(nvm.RawDirectory + "/" + )
                }
            });
            // Assign function for the NewWorkspaceWindowCommand.
            NewWorkspaceWindowCommand = new(() =>
            {
                // Get an instance of the MainWindow.
                MainWindow mw = (MainWindow)Application.Current.MainWindow;
                // Create an instance of the NewWorkspaceWindow and set its owner to be MainWindow and a new datacontext.
                NewWorkspaceWindowViewModel nvm = new();
                Window w = new NewWorkspaceWindow
                {
                    Owner = mw,
                    DataContext = nvm
                };
                // Show the NewWorkspaceWindow as a dialogue, meaning it will halt usage of the main program until the user closes it by clicking one of the buttons
                w.ShowDialog();
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
    }
}
