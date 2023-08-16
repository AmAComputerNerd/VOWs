using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;

namespace VOWs.MVVM.Model.Data
{
    public class Workspace : DataObject
    {
        // Fields.
        private Uri _location;
        private Document _selectedDocument;

        // Properties.
        /// <summary>
        /// The <c>Info</c> property stores information about this <c>Workspace</c>.
        /// </summary>
        public override DataObjectInfo Info { get; }
        /// <summary>
        /// The <c>Location</c> property holds a Uri to the location of this <c>Workspace</c> on the computer.
        /// </summary>
        public override Uri Location { get => _location; set => SetProperty(ref _location, value); }
        /// <summary>
        /// The <c>SelectedDocument</c> property holds the currently open <c>Document</c> from within this
        /// <c>Workspace</c>.
        /// </summary>
        public Document SelectedDocument { get => _selectedDocument; set => SetProperty(ref _selectedDocument, value); }

        /// <summary>
        /// The constructor for <c>Workspace</c> initialises variables based off the provided <c>location</c>.
        /// </summary>
        /// <param name="location">A Uri pointing to the location of this <c>Workspace</c> on the computer.</param>
        public Workspace(Uri location)
        {
            // TODO: Load the contents of the 'info.yml' file in the zipped folder.
            Info = new DataObjectInfo()
            {
                Name = new FileInfo(location.AbsolutePath).Name,
                Description = "This is an example description!",
                Extension = ExtensionUtils.GetType(new FileInfo(location.AbsolutePath).Extension)
            };
            Location = location;
            // For now, we'll just set the mode settings to the 'EnforceXXXMode' settings defined in EnvironmentArgs.
            CompatibilityMode = Globals.Default.CommandLineArgs.EnforceCompatibilityMode;
            TextOnlyMode = Globals.Default.CommandLineArgs.EnforceTextOnlyMode;
            ReadOnlyMode = Globals.Default.CommandLineArgs.EnforceReadOnlyMode;
        }
    }
}
