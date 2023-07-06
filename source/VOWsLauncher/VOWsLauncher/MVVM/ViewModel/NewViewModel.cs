using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows;
using VOWsLauncher.Events;
using VOWsLauncher.MVVM.Model;
using VOWsLauncher.SubWindows;

namespace VOWsLauncher.MVVM.ViewModel
{
    public class NewViewModel : ObservableRecipient
    {
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

        public NewViewModel() 
        {
            // Activate NewViewModel to receive messages.
            IsActive = true;

            // Assign function for the NewDocumentWindowCommand.
            NewDocumentWindowCommand = new(() =>
            {
                MainWindow mw = (MainWindow)Application.Current.MainWindow;
                Window w = new NewDocumentWindow();
                w.Owner = mw;
                w.DataContext = mw.DataContext;
                w.ShowDialog();
            });
            // Assign function for the NewWorkspaceWindowCommand.
            NewWorkspaceWindowCommand = new(() =>
            {
                MainWindow mw = (MainWindow)Application.Current.MainWindow;
                Window w = new NewWorkspaceWindow();
                w.Owner = mw;
                w.DataContext = mw.DataContext;
                w.ShowDialog();
            });
        }

        protected override void OnActivated()
        {
            Messenger.Register<NewViewModel, DisplayNewSubmenuMessage>(this, (r, m) => r.Receive(m));
        }

        private void Receive(DisplayNewSubmenuMessage message)
        {
            if (message.IsWorkspace) NewWorkspaceWindowCommand.Execute(null);
            else NewDocumentWindowCommand.Execute(null);
        }
    }
}
