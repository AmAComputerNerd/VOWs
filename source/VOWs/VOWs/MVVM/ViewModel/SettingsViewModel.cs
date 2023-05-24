using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using VOWs.Events;
using VOWs.MVVM.Model;

namespace VOWs.MVVM.ViewModel
{
    public class SettingsViewModel : ObservableRecipient
    {
        // Copies of global resources relevant to the SettingsView.
        public DatabaseWrapper Storage
        {
            get => Messenger.Send(new RequestStorageMessage());
            set => Messenger.Send(new UpdateStorageMessage(value));
        }
        // Local resources relevant to the SettingsView.

        public SettingsViewModel()
        {
        }
    }
}
