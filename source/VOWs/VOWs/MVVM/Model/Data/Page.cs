using CommunityToolkit.Mvvm.ComponentModel;
using System.Drawing;
using System.Windows.Documents;

namespace VOWs.MVVM.Model.Data
{
    public class Page : ObservableRecipient
    {
        private string _pageSize;
        private string _pageOrientation;
        private Color _pageColour;
        private FlowDocument _pageContent;

        public string PageSize { get => _pageSize; set => SetProperty(ref _pageSize, value); }
        public string PageOrientation { get => _pageOrientation; set => SetProperty(ref _pageOrientation, value); }
        public Color PageColour { get => _pageColour; set => SetProperty(ref _pageColour, value); }
        public FlowDocument PageContent { get => _pageContent; set => SetProperty(ref _pageContent, value); }

        public Page(string size)
        {
            
        }

    }
}
