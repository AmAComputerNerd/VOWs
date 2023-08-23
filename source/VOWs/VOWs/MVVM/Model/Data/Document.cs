using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.ObjectModel;
using System.IO;
using VOWs.Custom;
using VOWs.Events;

namespace VOWs.MVVM.Model.Data
{
    public class Document : DataObject
    {
        // Fields.
        private Uri _location;
        private ObservableCollection<Page> _pages;

        // Properties.
        /// <summary>
        /// The <c>Info</c> property stores information about this <c>Document</c>.
        /// </summary>
        public override DataObjectInfo Info { get; }
        /// <summary>
        /// The <c>Location</c> property holds a Uri to the location of this <c>Document</c> on the computer.
        /// </summary>
        public override Uri Location { get => _location; set => SetProperty(ref _location, value); }
        /// <summary>
        /// The <c>Pages</c> property refers to the current collection of <c>Page</c> objects in this <c>Document</c>.
        /// </summary>
        public ObservableCollection<Page> Pages { get => _pages; set => SetProperty(ref _pages, value); }

        /// <summary>
        /// The constructor for <c>Document</c> initialises variables based off the provided <c>location</c>.
        /// </summary>
        /// <param name="location">A Uri pointing to the location of this <c>Document</c> on the computer.</param>
        public Document(Uri location)
        {
            // TODO: Load the contents of the 'info.yml' file in the zipped folder.
            Info = new()
            {
                Name = new FileInfo(location.AbsolutePath).Name,
                Extension = ExtensionUtils.GetType(new FileInfo(location.AbsolutePath).Extension)
            };
            Location = location;
            Pages = new();
            // For now, we'll just set the mode settings to the 'EnforceXXXMode' settings defined in EnvironmentArgs.
            CompatibilityMode = Globals.Default.CommandLineArgs.EnforceCompatibilityMode;
            TextOnlyMode = Globals.Default.CommandLineArgs.EnforceTextOnlyMode;
            ReadOnlyMode = Globals.Default.CommandLineArgs.EnforceReadOnlyMode;
        }

        /// <summary>
        /// The <c>Save</c> method will save the data within this <c>Document</c> object back to a file format.
        /// Depending on the <c>ExtensionType</c>, some elements may be excluded or changed in this process.
        /// </summary>
        public void Save()
        {
            Save(Location);
        }

        /// <summary>
        /// The <c>Save</c> method will save the data within this <c>Document</c> object back to a file format
        /// within a new location. Depending on the <c>ExtensionType</c>, some elements may be excluded or changed
        /// in this process.
        /// </summary>
        /// <param name="newLocation">A Uri pointing to the new location for the Document.</param>
        public void Save(Uri newLocation)
        {
            if (!newLocation.IsFile)
            {
                // Failure to save - log an error.
                Messenger.Send(new LogMessage("Expected a 'File' type Uri - got 'Directory'. This may be related to a faulty method call.", ToString(), LogLevel.ERROR));
                // Then we'll throw an exception.
                throw new ArgumentException("Expected a 'File' type Uri - got 'Directory'. This may be related to a faulty method call.");
            }
            Save(newLocation, Path.GetFileNameWithoutExtension(newLocation.AbsolutePath), ExtensionUtils.GetType(new FileInfo(newLocation.AbsolutePath).Extension));
        }

        /// <summary>
        /// The <c>Save</c> method will save the data within this <c>Document</c> object back to a file format
        /// within a new location. Depending on the <c>ExtensionType</c>, some elements may be excluded or changed
        /// in this process.
        /// </summary>
        /// <param name="directory">A Uri pointing to the directory intended to house this Document.</param>
        /// <param name="fileName">A valid Windows file name for this new file, excl. the extension.</param>
        /// <param name="extensionType">An ExtensionType dictating what extension logic to use for saving this Document.</param>
        public bool Save(Uri directory, string fileName, ExtensionType extensionType)
        {
            if (directory.IsFile)
            {
                // We'll have to use the parent directory to the Uri we were given.
                // We'll log a warning to acknowledge this is happening.
                Messenger.Send(new LogMessage("Expected a 'Directory' type Uri - got 'File'. Using parent directory instead.", ToString(), LogLevel.WARNING));
                // Set 'directory' to the parent directory of the current Uri.
                directory = new(Directory.GetParent(directory.AbsolutePath).FullName);
            }
            // TODO: Save the file.
            return false;
        }
    }
}
