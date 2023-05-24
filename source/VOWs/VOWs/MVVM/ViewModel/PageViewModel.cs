using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Windows.Media;
using VOWs.Events;
using VOWs.MVVM.Model;

namespace VOWs.MVVM.ViewModel
{
    public class PageViewModel : ObservableRecipient
    {
        // Copies of global resources relevant to the PageView.
        private DatabaseWrapper _storage;
        public DatabaseWrapper Storage
        {
            get
            {
                try
                {
                    DatabaseWrapper retrievedWrapper = Messenger.Send(new RequestStorageMessage());
                    SetProperty(ref _storage, retrievedWrapper);
                }
                catch (Exception)
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

        // Local resources relevant to the PageView.
        private FontFamily _fontFamily;
        public FontFamily FontFamily
        {
            get => _fontFamily; 
            set => SetProperty(ref _fontFamily, value);
        }
        private int _fontSize;
        public int FontSize
        {
            get => _fontSize; 
            set => SetProperty(ref _fontSize, value);
        }
        private bool _bold;
        public bool Bold 
        {
            get => _bold;
            set => SetProperty(ref _bold, value); 
        }
        private bool _italics;
        public bool Italics
        {
            get => _italics;
            set => SetProperty(ref _italics, value);
        }
        private bool _underline;
        public bool Underline
        {
            get => _underline;
            set => SetProperty(ref _italics, value);
        }
        private Color _textColour;
        public Color TextColour
        {
            get => _textColour;
            set => SetProperty(ref _textColour, value);
        }
        private Color _backgroundColour;
        public Color BackgroundColour
        {
            get => _backgroundColour;
            set => SetProperty(ref _backgroundColour, value);
        }

        public PageViewModel()
        {
            // Set the backup storage variable.
            _storage = Messenger.Send(new RequestStorageMessage());
            // Set font properties.
            _fontFamily = new FontFamily("Calibri");
            _fontSize = 11;
            _bold = false;
            _italics = false;
            _underline = false;
            _textColour = Color.FromRgb(0, 0, 0);
            _backgroundColour = Color.FromRgb(255, 255, 255);
        }
    }
}
