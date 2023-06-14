using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;

namespace VOWs.MVVM.Model
{
    public partial class Document : ObservableRecipient
    {
        // Properties.
        /// <summary>
        /// The <c>Name</c> property represents the given name to this Document.
        /// </summary>
        [ObservableProperty]
        private string name;
        /// <summary>
        /// The <c>DefaultPageSize</c> property represents the default page size to give to new Pages associated with it.
        /// For more information on applicable values, see <c>Page.Size</c>.
        /// </summary>
        [ObservableProperty]
        private string defaultPageSize;
        /// <summary>
        /// The <c>Pages</c> property represents the collection of Pages that make up this document.
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<Page> pages;
        /// <summary>
        /// The <c>FileLocation</c> property represents the location of this Document on the computer.
        /// </summary>
        [ObservableProperty]
        private Uri fileLocation;
        // Attributes.
        /// <summary>
        /// The <c>DisplayPages</c> attribute represents the display objects (Border, TextBlock, etc.) that make up the Pages of this document.
        /// </summary>
        public ObservableCollection<object> DisplayPages
        {
            get
            {
                Collection<object> result = new Collection<object>();
                foreach (Page page in Pages) 
                {
                    foreach (object obj in page.DisplayElements)
                    {
                        result.Add(obj);
                    }
                }
                return new ObservableCollection<object>(result);
            }
        }

        /// <summary>
        /// The constructor for <c>Document</c> constructs the object based off the Uri pointing to a VOWsuite
        /// "Document" file.
        /// </summary>
        /// <param name="fileLocation">The Uri pointing to the VOWsuite "Document" file.</param>
        public Document(Uri fileLocation)
        {
            // TODO: Load document pages through Uri, if exists.
            Name = "ExampleName";
            DefaultPageSize = "A4";
            Pages = new();
            FileLocation = fileLocation;
        }

        /// <summary>
        /// Create a new Document at the defined <c>fileLocation</c>.
        /// If the <c>fileLocation</c> ends in a fileName, that name will be used as the name for this Document.
        /// If not, the Document will be named "UntitledX", where X is a number determined by the lowest
        /// available number that wouldn't conflict with other Document names in that directory.
        /// </summary>
        /// <param name="fileLocation">The string representing the file location. Rules for this variable are as defined above.</param>
        /// <returns>The created Document object.</returns>
        public static Document CreateNew(string fileLocation)
        {
            // TODO: Create a new Document in the specified location.
            return null;
        }

        /// <summary>
        /// Create a new Document at the defined <c>fileDirectory</c> under a defined <c>fileName</c>.
        /// The <c>fileDirectory</c> must point to an accessible directory, and the <c>fileName</c>
        /// must be a valid name for a file and must follow all Windows File Subsystem syntax rules.
        /// </summary>
        /// <param name="fileDirectory">The Uri pointing to the directory to store this Document in.</param>
        /// <param name="fileName">The name that will be used as the file name, as well the Document name.</param>
        /// <returns>The created Document object.</returns>
        public static Document CreateNew(Uri fileDirectory, string fileName)
        {
            // TODO: Create a new Document in the specified location.
            return null;
        }
    }
}
