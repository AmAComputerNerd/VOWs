using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows;
using VOWsLauncher.Events;
using VOWsLauncher.MVVM.Model;

namespace VOWsLauncher.MVVM.ViewModel
{
    public class HomeViewModel : ObservableRecipient
    {
        public Globals Globals { get => Globals.Default; }

        public Visibility BeginnersTipsVisibility { get => Globals.ShowBeginnersText ? Visibility.Visible : Visibility.Collapsed; }

        public RelayCommand NewDocumentCommand { get; }
        public RelayCommand NewWorkspaceCommand { get; }
        public RelayCommand OpenCommand { get; }
        public RelayCommand TemplatesCommand { get; }

        public HomeViewModel()
        {
            NewDocumentCommand = new(() =>
            {
                Messenger.Send(DisplayNewSubmenuMessage.Document());
            });
            NewWorkspaceCommand = new(() =>
            {
                Messenger.Send(DisplayNewSubmenuMessage.Workspace());
            });
            OpenCommand = new(() =>
            {
                Messenger.Send(new ChangeViewMessage(2)); // Open view
            });
            TemplatesCommand = new(() =>
            {
                Messenger.Send(new ChangeViewMessage(6)); // Templates view
            });
        }
    }
}
