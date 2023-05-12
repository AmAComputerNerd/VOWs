using System.Windows.Media.Converters;
using VOWsLauncher.Core;

namespace VOWsLauncher.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand NewViewCommand { get; set; }
        public RelayCommand OpenViewCommand { get; set; }
        public RelayCommand PinnedViewCommand { get; set; }
        public RelayCommand RecentViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public NewViewModel NewVM { get; set; }
        public OpenViewModel OpenVM { get; set; }
        public PinnedViewModel PinnedVM { get; set; }
        public RecentViewModel RecentVM { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel() 
        {
            HomeVM = new HomeViewModel();
            NewVM = new NewViewModel();
            OpenVM = new OpenViewModel();
            PinnedVM = new PinnedViewModel();
            RecentVM = new RecentViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            NewViewCommand = new RelayCommand(o =>
            {
                CurrentView = NewVM;
            });

            OpenViewCommand = new RelayCommand(o =>
            {
                CurrentView = OpenVM;
            });

            PinnedViewCommand = new RelayCommand(o =>
            {
                CurrentView = PinnedVM;
            });

            RecentViewCommand = new RelayCommand(o =>
            {
                CurrentView = RecentVM;
            });
        }
    }
}
