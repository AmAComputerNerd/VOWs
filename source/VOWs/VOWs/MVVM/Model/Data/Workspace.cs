using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;

namespace VOWs.MVVM.Model
{
    /// <summary>
    /// The <c>Workspace</c> class is a data representation of a VOWsuite workspace - a collection of documents that are in some ways related to each other, grouped by the user.
    /// This includes all information about the location of the Workspace, documents within it, and workspace-specific settings (later).
    /// </summary>
    public partial class Workspace : ObservableRecipient
    {
        /// <summary>
        /// The <c>Name</c> property represents the given name to this Workspace.
        /// </summary>
        [ObservableProperty]
        private string name;
        /// <summary>
        /// The <c>Documents</c> property represents the collection of Documents within this Workspace.
        /// These are generated based off all items matching the VOWsuite format within the Workspace.
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<Document> documents;
        /// <summary>
        /// The <c>FileLocation</c> property represents the location of this Workspace on the computer.
        /// </summary>
        [ObservableProperty]
        private Uri fileLocation;

        /// <summary>
        /// The constructor for <c>Workspace</c> constructs the object based off a Uri pointing to a VOWsuite
        /// "Workspace" file.
        /// </summary>
        /// <param name="fileLocation">The Uri pointing to the VOWsuite "Workspace" file.</param>
        public Workspace(Uri fileLocation)
        {
            FileLocation = fileLocation;
        }

        /// <summary>
        /// Create a new Workspace at the defined <c>fileLocation</c>.
        /// If the <c>fileLocation</c> ends in a fileName, that name will be used as the name for this Workspace.
        /// If not, the Workspace will be named "UntitledX", where X is a number determined by the lowest
        /// available number that wouldn't conflict with other Workspace names in that directory.
        /// </summary>
        /// <param name="fileLocation">The string representing the file location. Rules for this variable are as above.</param>
        /// <returns>The created Workspace object.</returns>
        public static Workspace CreateNew(string fileLocation)
        {
            // TODO: Create a new Workspace in the specified location.
            return null;
        }

        /// <summary>
        /// Create a new Workspace at the defined <c>fileDirectory</c> under a defined <c>fileName</c>.
        /// The <c>fileDirectory</c> must point to an accessible directory, and the <c>fileName</c>
        /// must be a valid name for a file and must follow all Windows File Subsystem syntax rules.
        /// </summary>
        /// <param name="fileDirectory">The Uri pointing to the directory to store this Workspace in.</param>
        /// <param name="fileName">The name that will be used as the file name, as well as the Workspace name.</param>
        /// <returns>The created Workspace object.</returns>
        public static Workspace CreateNew(Uri fileDirectory, string fileName)
        {
            // TODO: Create a new Workspace in the specified location.
            return null;
        }
    }
}
