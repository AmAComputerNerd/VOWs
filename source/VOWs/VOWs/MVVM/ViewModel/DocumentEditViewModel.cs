using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VOWs.Events;
using VOWs.MVVM.Model;

namespace VOWs.MVVM.ViewModel
{
    public class DocumentEditViewModel
    {
        // Copies of global resources relevant to the DocumentEditView.
        public DatabaseWrapper Storage { get; set; }

        // Local resources relevant to the DocumentEditView.
        private DynamicURIResolver _vowsuiteLogoUriResolver;
        public ImageSource VOWsuiteLogoSource { get => new BitmapImage(_vowsuiteLogoUriResolver.Uri); }

        public RelayCommand VOWsuiteButtonCommand;

        public DocumentEditViewModel(MainViewModel context)
        {
            Storage = context.Storage;
            // Set the Logo URI Resolver, a fancy little solution that can automatically return a relevant URI depending on the theme selected, or just a backup if the main one can't be resolved.
            string _whiteLogoSource = "pack://application:,,,/Resources/Images/VOWsuite-logos_white.png";
            string _blackLogoSource = "pack://application:,,,/Resources/Images/VOWsuite-logos_black.png";
            _vowsuiteLogoUriResolver = new DynamicURIResolver(_whiteLogoSource, _whiteLogoSource, _blackLogoSource, _blackLogoSource, Storage, new Uri("Resources/Images/VOWsuite-logos_white.png", UriKind.Relative));
            // Set VOWsuiteButtonCommand to send a message when clicked.
            VOWsuiteButtonCommand = new RelayCommand(() =>
            {
                WeakReferenceMessenger.Default.Send<ChangeViewEvent, string>("VOWsButtonToggle");
            });
        }
    }
}
