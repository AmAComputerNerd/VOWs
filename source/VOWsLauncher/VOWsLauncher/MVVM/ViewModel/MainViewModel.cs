using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows;
using System.Windows.Media;
using VOWsLauncher.MVVM.Model;

namespace VOWsLauncher.MVVM.ViewModel
{
    public class MainViewModel : ObservableRecipient
    {
        public Globals Globals { get => Globals.Default; }
        public DynamicURIResolver VOWsuiteLogo { get; }

        public RelayCommand WinNavMinimiseCommand { get; }
        public RelayCommand WinNavCloseCommand { get; }

        public RelayCommand HomeViewCommand { get; }
        public RelayCommand NewViewCommand { get; }
        public RelayCommand OpenViewCommand { get; }
        public RelayCommand PinnedViewCommand { get; }
        public RelayCommand RecentViewCommand { get; }
        public RelayCommand SettingsViewCommand { get; }

        public HomeViewModel HomeVM;
        public NewViewModel NewVM;
        public OpenViewModel OpenVM;
        public PinnedViewModel PinnedVM;
        public RecentViewModel RecentVM;
        public SettingsViewModel SettingsVM;

        private object _currentView;
        public object CurrentView { get => _currentView; set => SetProperty(ref _currentView, value); }


        public MainViewModel() 
        {
            // Activate MainViewModel to receive messages.
            IsActive = true;

            // Set the Logo URI Resolver, a fancy little solution that can automatically return a relevant URI depending on the theme selected, or just a backup if the main one can't be resolved.
            VOWsuiteLogo = new("pack://application:,,,/Resources/Images/VOWsuite-logos_white.png",
                "pack://application:,,,/Resources/Images/VOWsuite-logos_black.png",
                new Uri("/Resources/Images/VOWsuite-logos_black.png", UriKind.Relative));

            // Assign function for WinNav commands.
            WinNavMinimiseCommand = new RelayCommand(() => {
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            });
            WinNavCloseCommand = new RelayCommand(() =>
            {
                Application.Current.Shutdown();
            });

            // Assign values for Home view.
            HomeVM = new HomeViewModel();
            HomeViewCommand = new RelayCommand(() =>
            {
                CurrentView = HomeVM;
            });
            // Assign values for New view.
            NewVM = new NewViewModel();
            NewViewCommand = new RelayCommand(() =>
            {
                CurrentView = NewVM;
            });
            // Assign values for Open view.
            OpenVM = new OpenViewModel();
            OpenViewCommand = new RelayCommand(() =>
            {
                CurrentView = OpenVM;
            });
            // Assign values for Pinned view.
            PinnedVM = new PinnedViewModel();
            PinnedViewCommand = new RelayCommand(() =>
            {
                CurrentView = PinnedVM;
            });
            // Assign values for Recent view.
            RecentVM = new RecentViewModel();
            RecentViewCommand = new RelayCommand(() =>
            {
                CurrentView = RecentVM;
            });
            // Assign values for Settings view.
            SettingsVM = new SettingsViewModel();
            SettingsViewCommand = new RelayCommand(() =>
            {
                CurrentView = SettingsVM;
            });

            // Set CurrentView to default menu (HomeVM).
            CurrentView = HomeVM;
        }
    }
}
