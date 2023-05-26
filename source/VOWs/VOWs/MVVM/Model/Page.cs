using CommunityToolkit.Mvvm.ComponentModel;

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
        /// The <c>CursorLineNum</c> property represents the current line the cursor is focusing on (starting at 1).
        /// </summary>
        [ObservableProperty]
        private int cursorLineNum;
        /// <summary>
        /// The <c>CursorLinePos</c> property represents the current position within a line the cursor is focusing on (starting at 0).
        /// </summary>
        [ObservableProperty]
        private int cursorLinePos;

        /// <summary>
        /// The constructor for <c>Page</c> constructs the object based off the arguments entered.
        /// </summary>
        /// <param name="size">The page size, following standard international paper sizes.</param>
        /// <param name="orientation">The orientation, either <c>horizontal</c> and <c>vertical</c>.</param>
        /// <param name="cursorLineNum">The line number the cursor currently sits at on this page.</param>
        /// <param name="cursorLinePos">The position within the line the cursor currently sits at on this page.</param>
        public Page(string size, string orientation, int cursorLineNum, int cursorLinePos)
        {
            Size = size;
            Orientation = orientation;
            CursorLineNum = cursorLineNum;
            CursorLinePos = cursorLinePos;
        }
    
        /// <summary>
        /// Create a new Page with default settings.
        /// <b>NOTE: A Page requires a parent Document to be displayed.</b>
        /// </summary>
        /// <returns>The new A4, vertical Page.</returns>
        public static Page Default()
        {
            return new Page("a4", "vertical", 1, 0);
        }

        /// <summary>
        /// Create a new Page with Slide settings.
        /// <b>NOTE: A Page requires a parent Document to be displayed.</b>
        /// </summary>
        /// <returns>The new A4, horizontal Page.</returns>
        public static Page Slide()
        {
            return new Page("a4", "horizontal", 1, 0);
        }
    }
}
