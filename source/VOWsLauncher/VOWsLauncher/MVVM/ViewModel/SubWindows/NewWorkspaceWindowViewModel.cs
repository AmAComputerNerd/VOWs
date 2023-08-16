using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VOWsLauncher.Events;
using VOWsLauncher.MVVM.Model;

namespace VOWsLauncher.MVVM.ViewModel.SubWindows
{
    public class NewWorkspaceWindowViewModel : ObservableObject
    {
        public Globals Globals { get => Globals.Default; }

        public RelayCommand BrowseDirCommand { get; }
        public RelayCommand CreateCommand { get; }
        public RelayCommand CancelCommand { get; }

        #region XAMLBindings

        // Fields
        private string _workspaceName = "Unnamed Workspace";
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
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                Uri.TryCreate(RawDirectory, UriKind.Absolute, out Uri uri);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8603 // Possible null reference return.
                return uri;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }
        /// <summary>
        /// The <c>Template</c> property refers to the current template for this new workspace.
        /// It is retrieved using inter-app messaging, and will default to an empty Template object if none is set.
        /// </summary>
        public Template Template { get => WeakReferenceMessenger.Default.Send(new RetrieveSelectedTemplateMessage()); }

        #endregion XAMLBindings

        public NewWorkspaceWindowViewModel(Window w)
        {
            BrowseDirCommand = new(() =>
            {
#pragma warning disable CS8601 // Possible null reference assignment.
                RawDirectory = WeakReferenceMessenger.Default.Send(new DisplayOpenSubmenuMessage(TargetType.DIRECTORY)) ?? "";
#pragma warning restore CS8601 // Possible null reference assignment.
            });
            CreateCommand = new(() =>
            {
                // TODO: Validation of data.
                w.DialogResult = true;
                w.Close();
            });
            CancelCommand = new(() =>
            {
                w.DialogResult = false;
                w.Close();
            });
        }
    }
}
