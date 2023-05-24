using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace VOWs.MVVM.Model
{
    public class Font : ObservableRecipient
    {
        private FontFamily _name;
        private int _size;
        private bool _bold;
        private bool _italics;
        private bool _underline;
        private Color _foreground;
        private Color _background;

        public FontFamily Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public int Size
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }
        public bool Bold
        {
            get => _bold;
            set => SetProperty(ref _bold, value);
        }
        public bool Italics
        {
            get => _italics;
            set => SetProperty(ref _italics, value);
        }
        public bool Underline
        {
            get => _underline;
            set => SetProperty(ref _italics, value);
        }
        public Color Foreground
        {
            get => _foreground;
            set => SetProperty(ref _foreground, value);
        }
        public Color Background
        {
            get => _background;
            set => SetProperty(ref _background, value);
        }
    
        public Font()
        {
            _name = new FontFamily("Calibri");
            _size = 11;
            _bold = false;
            _italics = false;
            _underline = false;
            _foreground = Color.FromRgb(0, 0, 0);
            _background = Color.FromRgb(255, 255, 255);
        }

        public Font(FontFamily name) : this()
        {
            _name = name;
        }

        public Font(FontFamily name, int size, Color foreground) : this(name)
        {
            _size = size;
            _foreground = foreground;
        }

        public Font(FontFamily name, int size, bool bold, bool italics, bool underline, Color foreground, Color background) : this(name, size, foreground)
        {
            _bold = bold;
            _italics = italics;
            _underline = underline;
            _background = background;
        }
    }
}
