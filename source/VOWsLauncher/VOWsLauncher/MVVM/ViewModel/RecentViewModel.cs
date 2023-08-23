using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;
using VOWsLauncher.MVVM.Model;

namespace VOWsLauncher.MVVM.ViewModel
{
    public class RecentViewModel : ObservableObject
    {
        public Globals Globals { get => Globals.Default; }

        // Fields
        private bool _isWorkspaceSelected;
        private bool _isDocumentSelected;
        private Visibility _workspaceContentVisibility;
        private Visibility _documentContentVisibility;

        public bool IsWorkspaceSelected 
        { 
            get => _isWorkspaceSelected; 
            set
            {
                if (value) WorkspaceContentVisibility = Visibility.Visible;
                SetProperty(ref _isWorkspaceSelected, value);
            } 
        }
        public bool IsDocumentSelected 
        { 
            get => _isDocumentSelected;
            set
            {
                if (value) DocumentContentVisibility = Visibility.Visible;
                SetProperty(ref _isDocumentSelected, value);
            }
        }
        public Visibility WorkspaceContentVisibility 
        { 
            get => _workspaceContentVisibility; 
            set
            {
                if (value == Visibility.Visible) DocumentContentVisibility = Visibility.Collapsed;
                SetProperty(ref _workspaceContentVisibility, value);
            }
        }
        public Visibility DocumentContentVisibility
        {
            get => _documentContentVisibility;
            set
            {
                if (value == Visibility.Visible) WorkspaceContentVisibility = Visibility.Collapsed;
                SetProperty(ref _documentContentVisibility, value);
            }
        }

        public RecentViewModel()
        {
            IsWorkspaceSelected = true;
        }
    }
}
