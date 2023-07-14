using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace VOWs.MVVM.Model.Data
{
    public class Document : ObservableRecipient
    {
        private string _name;
        private Uri _location;
        private string _defaultPageSize;
        private string _defaultPageOrientation;
        private ObservableCollection<Page> _pages;

        /// <summary>
        /// The <c>Name</c> property represents the given name to this Document.
        /// </summary>
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        /// <summary>
        /// The <c>Location</c> property represents the location of this Document on the computer.
        /// </summary>
        public Uri Location { get => _location; set => SetProperty(ref _location, value); }
        /// <summary>
        /// The <c>DefaultPageSize</c> property represents the default page size to give to new Pages associated with it.
        /// For more information on applicable values, see <c>Page.Size</c>.
        /// </summary>
        public string DefaultPageSize { get => _defaultPageSize; set => SetProperty(ref _defaultPageSize, value); }
        /// <summary>
        /// The <c>DefaultPageOrientation</c> property represents the default page orientation to give to new Pages associated with it.
        /// For more information on applicable values, see <c>Page.Orientation</c>
        /// </summary>
        public string DefaultPageOrientation { get => _defaultPageOrientation; set => SetProperty(ref _defaultPageOrientation, value); }
        /// <summary>
        /// The <c>Pages</c> property represents the collection of Pages that make up this document.
        /// </summary>
        public ObservableCollection<Page> Pages { get => _pages; set => SetProperty(ref _pages, value); }

        /// <summary>
        /// The constructor for <c>Document</c> constructs the object based off the Uri pointing to a VOWsuite
        /// "Document" file.
        /// </summary>
        /// <param name="location">The Uri pointing to the VOWsuite "Document" file.</param>
        public Document(Uri location)
        {
            Location = location;
            // TODO: Load data from location 'settings.yml' file.
            Name = "Example Document";
            DefaultPageSize = "a4";
            DefaultPageOrientation = "vertical";
            Pages = new()
            {
                new Page("a4")
            };
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
            // TODO: Create a document in the specified location.
            return null;
        }
    }
}
