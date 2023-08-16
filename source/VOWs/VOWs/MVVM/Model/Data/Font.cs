using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Media;

namespace VOWs.MVVM.Model.Data
{
    public class Font : ObservableRecipient
    {
        // Fields.
        private FontFamily _family;
        private int _size;
        private bool _bold;
        private bool _italics;
        private bool _underline;
        private bool _strikethrough;
        private Color _foreground;
        private Color _background;

        // Properties.
        /// <summary>
        /// The <c>Family</c> property represents the FontFamily for this Font object.
        /// </summary>
        public FontFamily Family { get => _family; set => SetProperty(ref _family, value); }
        /// <summary>
        /// The <c>Size</c> property represents the font size for this Font object.
        /// </summary>
        public int Size { get => _size; set => SetProperty(ref _size, value); }
        /// <summary>
        /// The <c>Bold</c> property represents whether this Font object is bold or not.
        /// </summary>
        public bool Bold { get => _bold; set => SetProperty(ref _bold, value); }
        /// <summary>
        /// The <c>Italics</c> property represents whether this Font object is italic or not.
        /// </summary>
        public bool Italics { get => _italics; set => SetProperty(ref _italics, value); }
        /// <summary>
        /// The <c>Underline</c> property represents whether this Font object is underlined or not.
        /// </summary>
        public bool Underline { get => _underline; set => SetProperty(ref _underline, value); }
        /// <summary>
        /// The <c>Strikethrough</c> property represents whether this Font object is struckthrough or not.
        /// </summary>
        public bool Strikethrough { get => _strikethrough; set => SetProperty(ref _strikethrough, value); }
        /// <summary>
        /// The <c>Foreground</c> property represents the colour for the foreground (text) for this Font object.
        /// </summary>
        public Color Foreground { get => _foreground; set => SetProperty(ref _foreground, value); }
        /// <summary>
        /// The <c>Background</c> property represents the colour for the background (behind text) for this Font object.
        /// </summary>
        public Color Background { get => _background; set => SetProperty(ref _background, value); }

        /// <summary>
        /// The constructor for <c>Font</c> constructs the object based off the <c>name</c> (FontFamily) parameter,
        /// with otherwise default settings.
        /// </summary>
        /// <param name="family">The FontFamily that this Font should use.</param>
        public Font(FontFamily family)
        {
            Family = family;
            Size = 11;
            Bold = false;
            Italics = false;
            Underline = false;
            Foreground = Color.FromRgb(0, 0, 0);
            Background = Color.FromRgb(255, 255, 255);
        }

        /// <summary>
        /// The constructor for <c>Font</c> constructs the object based off the <c>family</c>, <c>size</c>
        /// and <c>foreground</c> parameters, with otherwise default settings.
        /// </summary>
        /// <param name="family">The FontFamily that this Font should use.</param>
        /// <param name="size">The size that this Font should use.</param>
        /// <param name="foreground">The foreground that this Font should use.</param>
        public Font(FontFamily family, int size, Color foreground) : this(family)
        {
            Size = size;
            Foreground = foreground;
        }

        /// <summary>
        /// The constructor for <c>Font</c> constructs the object based off parameters entered.
        /// </summary>
        /// <param name="family">The FontFamily that this Font should use.</param>
        /// <param name="size">The size that this Font should use.</param>
        /// <param name="bold">Whether this Font should be bold.</param>
        /// <param name="italics">Whether this Font should be italics.</param>
        /// <param name="underline">Whether this Font should be underlined.</param>
        /// <param name="foreground">The Color this Font should use as a foreground (text colour).</param>
        /// <param name="background">The Color this Font should use as a background (text highlight/background).</param>
        public Font(FontFamily family, int size, bool bold, bool italics, bool underline, Color foreground, Color background) : this(family, size, foreground)
        {
            Bold = bold;
            Italics = italics;
            Underline = underline;
            Background = background;
        }

        /// <summary>
        /// Create a new Font with default settings.
        /// </summary>
        /// <returns>The new Calibri font, size 11 without any modifiers, with a black foreground and white background.</returns>
        public static Font Default()
        {
            return new(new("Calibri"));
        }
    }
}