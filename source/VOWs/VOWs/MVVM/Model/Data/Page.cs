using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.DirectoryServices.ActiveDirectory;
using System.Windows;
using System;
using System.Security.Cryptography.Xml;
using System.Windows.Input;

namespace VOWs.MVVM.Model
{
    /// <summary>
    /// The <c>Page</c> class is a data representation of a page within a VOWsuite document.
    /// This includes all information about a particular page (e.g. size, orientation, content, etc.)
    /// </summary>
    public partial class Page : ObservableRecipient
    {
        // Fields.
        private string _size;
        private string _orientation;
        private Color _pageColour;
        private RichTextBox _text;
        private Font _selectedFont;
        // Properties.
        /// <summary>
        /// The <c>Size</c> property represents the size of this page (e.g. A4, A3, etc.).
        /// </summary>
        public string Size { get => _size; set => SetProperty(ref _size, value); }
        /// <summary>
        /// The <c>Orientation</c> property represents the orientation of this page (either 'horizontal' or 'vertical').
        /// </summary>
        public string Orientation { get => _orientation; set => SetProperty(ref _orientation, value); }
        /// <summary>
        /// The <c>PageColour</c> property represents the colour for this page (default: White (255,255,255)).
        /// </summary>
        public Color PageColour { get => _pageColour; set => SetProperty(ref _pageColour, value); }
        /// <summary>
        /// The <c>Text</c> property represents the RichTextBox for this page, which will contain all formatted
        /// text. Writes should be handled with settings from the <c>Font</c> in mind.
        /// </summary>
        public RichTextBox Text { get => _text; set => SetProperty(ref _text, value); }
        /// <summary>
        /// The <c>SelectedFont</c> property represents the currently selected font settings. This is commonly updated
        /// by the overbearing <c>Document</c> object.
        /// </summary>
        public Font SelectedFont { get => _selectedFont; set => SetProperty(ref _selectedFont, value); }
        /// <summary>
        /// The <c>DisplayElements</c> property returns a XAML-friendly collection of WPF objects to display this page.
        /// </summary>
        public ObservableCollection<object> DisplayElements
        {
            get
            {
                Collection<object> result = new();
                Border b = new();
                if (Defaults.GetScaledPageDimensions(Size, Orientation, out int[] dimensions))
                {
                    b.Width = dimensions[0];
                    b.Height = dimensions[1];
                }
                b.Padding = new(20);
                b.Background = new SolidColorBrush(PageColour);
                b.Child = Text;
                result.Add(b);
                return new ObservableCollection<object>(result);
            }
        }

        /// <summary>
        /// The constructor for <c>Page</c> constructs the object based off the arguments entered.
        /// </summary>
        /// <param name="size">The page size, following standard international paper sizes.</param>
        /// <param name="orientation">The orientation, either <c>horizontal</c> and <c>vertical</c>.</param>
        /// <param name="font">The font to use when writing on this page.</param>
        public Page(string size, string orientation, Font font)
        {
            Size = size;
            Orientation = orientation;
            Text = new RichTextBox();
            // Start RichTextBox customisation.
            int[] dimensions = new int[2];
            if(Defaults.GetScaledPageDimensions(Size, Orientation, out dimensions))
            {
                Text.Width = dimensions[0] - 2*20;
                Text.Height = dimensions[1] - 2*20;
            }
            Text.IsUndoEnabled = false;
            // End.
            PageColour = Color.FromRgb(255, 255, 255);
            SelectedFont = font;
        }
    
        /// <summary>
        /// Create a new Page with default settings.
        /// <b>NOTE: A Page requires a parent Document to be displayed.</b>
        /// </summary>
        /// <returns>The new A4, vertical Page.</returns>
        public static Page Default()
        {
            return new Page("a4", "vertical", Font.Default());
        }

        /// <summary>
        /// Create a new Page with Slide settings.
        /// <b>NOTE: A Page requires a parent Document to be displayed.</b>
        /// </summary>
        /// <returns>The new A4, horizontal Page.</returns>
        public static Page Slide()
        {
            return new Page("a4", "horizontal", Font.Default());
        }
    }
}
