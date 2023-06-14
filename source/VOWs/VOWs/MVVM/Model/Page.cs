using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace VOWs.MVVM.Model
{
    public partial class Page : ObservableRecipient
    {
        /// <summary>
        /// The <c>Size</c> property represents the size of this page (e.g. A4, A3, etc.).
        /// </summary>
        [ObservableProperty]
        private string size;
        /// <summary>
        /// The <c>Orientation</c> property represents the orientation of this page (either 'horizontal' or 'vertical').
        /// </summary>
        [ObservableProperty]
        private string orientation;
        /// <summary>
        /// The <c>PageColour</c> property represents the colour for this page (default: White (255,255,255)).
        /// </summary>
        [ObservableProperty]
        private Color pageColour;
        /// <summary>
        /// The <c>Text</c> property represents the RichTextBox for this page, which will contain all formatted
        /// text. Writes should be handled with settings from the <c>Font</c> in mind.
        /// </summary>
        [ObservableProperty]
        private RichTextBox text;
        /// <summary>
        /// The <c>SelectedFont</c> property represents the currently selected font settings. This is commonly updated
        /// by the overbearing <c>Document</c> object.
        /// </summary>
        [ObservableProperty]
        private Font selectedFont;
        // Attributes.
        public Collection<object> DisplayElements
        {
            get
            {
                Collection<object> result = new Collection<object>();
                Border b = new Border();
                return result;
            }
        }

        /// <summary>
        /// The constructor for <c>Page</c> constructs the object based off the arguments entered.
        /// </summary>
        /// <param name="size">The page size, following standard international paper sizes.</param>
        /// <param name="orientation">The orientation, either <c>horizontal</c> and <c>vertical</c>.</param>
        /// <param name="cursorLineNum">The line number the cursor currently sits at on this page.</param>
        /// <param name="cursorLinePos">The position within the line the cursor currently sits at on this page.</param>
        public Page(string size, string orientation, Font font)
        {
            Size = size;
            Orientation = orientation;
            Text = new RichTextBox();
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
