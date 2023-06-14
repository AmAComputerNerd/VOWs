using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Media;

namespace VOWs.MVVM.Model
{
    /// <summary>
    /// The <c>Font</c> class is a data representation of the current font settings.
    /// </summary>
    public partial class Font : ObservableRecipient
    {
        private FontFamily _name;
        private int _size;
        private bool _bold;
        private bool _italics;
        private bool _underline;
        private Color _foreground;
        private Color _background;
        /// <summary>
        /// The <c>Name</c> property represents the FontFamily for this Font object.
        /// </summary>
        public FontFamily Name;
        /// <summary>
        /// The <c>Size</c> property represents the font size for this Font object.
        /// </summary>
        public int Size;
        /// <summary>
        /// The <c>Bold</c> property represents whether this Font object is bold or not.
        /// </summary>
        public bool Bold;
        /// <summary>
        /// The <c>Italics</c> property represents whether this Font object is italic or not.
        /// </summary>
        public bool Italics;
        /// <summary>
        /// The <c>Underline</c> property represents whether this Font object is underlined or not.
        /// </summary>
        public bool Underline;
        /// <summary>
        /// The <c>Foreground</c> property represents the colour for the foreground (text) for this Font object.
        /// </summary>
        public Color Foreground;
        /// <summary>
        /// The <c>Background</c> property represents the colour for the background (behind text) for this Font object.
        /// </summary>
        public Color Background;

        /// <summary>
        /// The constructor for <c>Font</c> constructs the object based off the <c>name</c> (FontFamily) parameter,
        /// with otherwise default settings.
        /// </summary>
        /// <param name="name">The FontFamily that this Font should use.</param>
        public Font(FontFamily name)
        {
            Name = name;
            Size = 11;
            Bold = false;
            Italics = false;
            Underline = false;
            Foreground = Color.FromRgb(0, 0, 0);
            Background = Color.FromRgb(255, 255, 255);
        }

        /// <summary>
        /// The constructor for <c>Font</c> constructs the object based off the <c>name</c>, <c>size</c>
        /// and <c>foreground</c> parameters, with otherwise default settings.
        /// </summary>
        /// <param name="name">The FontFamily that this Font should use.</param>
        /// <param name="size">The size that this Font should use.</param>
        /// <param name="foreground">The foreground that this Font should use.</param>
        public Font(FontFamily name, int size, Color foreground) : this(name)
        {
            Size = size;
            Foreground = foreground;
        }

        /// <summary>
        /// The constructor for <c>Font</c> constructs the object based off parameters entered.
        /// </summary>
        /// <param name="name">The FontFamilt that this Font should use.</param>
        /// <param name="size">The size that this Font should use.</param>
        /// <param name="bold">Whether this Font should be bold.</param>
        /// <param name="italics">Whether this Font should be italics.</param>
        /// <param name="underline">Whether this Font should be underlined.</param>
        /// <param name="foreground">The Color this Font should use as a foreground (text colour).</param>
        /// <param name="background">The Color this Font should use as a background (text highlight/background).</param>
        public Font(FontFamily name, int size, bool bold, bool italics, bool underline, Color foreground, Color background) : this(name, size, foreground)
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
            return new Font(new FontFamily("Calibri"));
        }
    }
}