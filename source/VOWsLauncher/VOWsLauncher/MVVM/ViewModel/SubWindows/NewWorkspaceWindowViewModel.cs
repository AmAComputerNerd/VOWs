using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using VOWsLauncher.Events;
using VOWsLauncher.MVVM.Model;

namespace VOWsLauncher.MVVM.ViewModel.SubWindows
{
    public class NewWorkspaceWindowViewModel : ObservableObject
    {
        // Fields
        private string _workspaceName = "Unnamed";
        private string _rawDirectory = "";

        // Modifiable properties
        /// <summary>
        /// The <c>WorkspaceName</c> property refers to the name for this workspace.
        /// This does not neccessarily need to follow Windows file name rules and will remain the name of the document in the Info tab,
        /// but the file name will be adjusted accordingly.
        /// </summary>
        public string WorkspaceName { get => _workspaceName; set => SetProperty(ref _workspaceName, value); }
        /// <summary>
        /// The <c>RawDirectory</c> property refers to the path leading to the directory for this new workspace.
        /// This path should be <c>absolute</c> and follow Windows File Directory format.
        /// </summary>
        public string RawDirectory { get => _rawDirectory; set => SetProperty(ref _rawDirectory, value); }

        // Unmodifiable / helper properties
        /// <summary>
        /// The <c>Directory</c> property refers to the converted Uri of the directory for this new workspace.
        /// If <c>RawDirectory</c> is not a valid Uri, null will instead be returned.
        /// </summary>
        public Uri Directory
        {
            get
            {
                Uri.TryCreate(RawDirectory, UriKind.Absolute, out Uri uri);
                return uri;
            }
        }
        /// <summary>
        /// The <c>Template</c> property refers to the current template for this new workspace.
        /// It is retrieved using inter-app messaging, and will default to an empty Template object if none is set.
        /// </summary>
        public Template Template { get => WeakReferenceMessenger.Default.Send(new RetrieveSelectedTemplateMessage()); }
    }
}
