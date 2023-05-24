using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VOWs.Events;
using VOWs.MVVM.Model;

namespace VOWs.MVVM.ViewModel
{
    public class DocumentEditViewModel : ObservableRecipient
    {
        // Copies of global resources relevant to the DocumentEditView.
        private DatabaseWrapper _storage;
        public DatabaseWrapper Storage
        {
            get
            {
                try
                {
                    DatabaseWrapper retrievedWrapper = Messenger.Send(new RequestStorageMessage());
                    SetProperty(ref _storage, retrievedWrapper);
                } catch (Exception)
                {
                    // RequestStorageMessage was never answered (likely due to being called at startup).
                    // We'll return our saved copy of _storage in this case.
                }
                return _storage;
            }
            set
            {
                // Send the UpdateStorageMessage and save the new value into _storage.
                bool updated = Messenger.Send(new UpdateStorageMessage(value));
                // If Storage was never updated, we shouldn't update our local variable here.
                if (updated) SetProperty(ref _storage, value);
                else throw new ArgumentException("Storage variable was unable to be updated.");
            }
        }

        // Local resources relevant to the DocumentEditView.
        private DynamicURIResolver _vowsuiteLogoUriResolver;
        public ImageSource VOWsuiteLogoSource { get => new BitmapImage(_vowsuiteLogoUriResolver.Uri); }
        private DynamicURIResolver _exampleImageUriResolver;
        public ImageSource ExampleImageSource { get => new BitmapImage(_vowsuiteLogoUriResolver.Uri); }

        public RelayCommand VOWsuiteButtonCommand { get; }
        public PageViewModel PageVM { get; }

        public DocumentEditViewModel()
        {
            // Set the backup storage variable.
            _storage = Messenger.Send(new RequestStorageMessage());
            // Set the Logo URI Resolver, a fancy little solution that can automatically return a relevant URI depending on the theme selected, or just a backup if the main one can't be resolved.
            string _whiteLogoSource = "pack://application:,,,/Resources/Images/VOWsuite-logos_white.png";
            string _blackLogoSource = "pack://application:,,,/Resources/Images/VOWsuite-logos_black.png";
            _vowsuiteLogoUriResolver = new DynamicURIResolver(_whiteLogoSource, _blackLogoSource, new Uri("Resources/Images/VOWsuite-logos_white.png", UriKind.Relative));
            // Set VOWsuiteButtonCommand to send a message when clicked.
            VOWsuiteButtonCommand = new RelayCommand(() =>
            {
                // Send a ChangeViewMessage message to the MainViewModel.
                Messenger.Send(new ChangeViewMessage(0, 1));
            });
        }
    }
}
