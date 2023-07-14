using CommunityToolkit.Mvvm.ComponentModel;
using VOWsLauncher.MVVM.Model;

namespace VOWsLauncher.MVVM.ViewModel
{
    public class RecentViewModel : ObservableObject
    {
        public Globals Globals { get => Globals.Default; }

        // Fields
        private bool _isWorkspaceSelected;
        private bool _isDocumentSelected;

        public bool IsWorkspaceSelected { get => _isWorkspaceSelected; set => SetProperty(ref _isWorkspaceSelected, value); }
        public bool IsDocumentSelected { get => _isDocumentSelected; set => SetProperty(ref _isDocumentSelected, value); }

        public RecentViewModel()
        {
            IsWorkspaceSelected = true;
        }
    }
}
