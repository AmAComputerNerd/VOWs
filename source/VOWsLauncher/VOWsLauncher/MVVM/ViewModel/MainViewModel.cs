using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Windows;
using VOWsLauncher.Events;
using VOWsLauncher.MVVM.Model;

namespace VOWsLauncher.MVVM.ViewModel
{
    public class MainViewModel : ObservableRecipient
    {
        /// <summary>
        /// The <c>Globals</c> property refers to an object full of shared variables across multiple classes.
        /// These may be read-only, or be able to be edited across classes. The <c>Globals</c> class is an <c>ObservableObject</c>.
        /// </summary>
        public Globals Globals { get => Globals.Default; }
        /// <summary>
        /// The <c>VOWsuiteLogo</c> property is a <c>DynamicURIResolver</c> holding themed URIs for the VOWsuite logo.
        /// </summary>
        public DynamicURIResolver VOWsuiteLogo { get; }

        /// <summary>
        /// The <c>WinNavMinimiseCommand</c> command will cause the application to minimise.
        /// </summary>
        public RelayCommand WinNavMinimiseCommand { get; }
        /// <summary>
        /// The <c>WinNavCloseCommand</c> command will cause the application to close.
        /// </summary>
        public RelayCommand WinNavCloseCommand { get; }

        /// <summary>
        /// The <c>HomeViewCommand</c> command will trigger a change in the <c>CurrentView</c> property, setting it to <c>HomeVM</c>.
        /// </summary>
        public RelayCommand HomeViewCommand { get; }
        /// <summary>
        /// The <c>NewViewCommand</c> command will trigger a change in the <c>CurrentView</c> property, setting it to <c>NewVM</c>.
        /// </summary>
        public RelayCommand NewViewCommand { get; }
        /// <summary>
        /// The <c>OpenViewCommand</c> command will trigger a change in the <c>CurrentView</c> property, setting it to <c>OpenVM</c>.
        /// </summary>
        public RelayCommand OpenViewCommand { get; }
        /// <summary>
        /// The <c>PinnedViewCommand</c> command will trigger a change in the <c>CurrentView</c> property, setting it to <c>PinnedVM</c>.
        /// </summary>
        public RelayCommand PinnedViewCommand { get; }
        /// <summary>
        /// The <c>RecentViewCommand</c> command will trigger a change in the <c>CurrentView</c> property, setting it to <c>RecentVM</c>.
        /// </summary>
        public RelayCommand RecentViewCommand { get; }
        /// <summary>
        /// The <c>SettingsViewCommand</c> command will trigger a change in the <c>CurrentView</c> property, setting it to <c>SettingsVM</c>.
        /// </summary>
        public RelayCommand SettingsViewCommand { get; }
        /// <summary>
        /// The <c>TemplatesViewCommand</c> command will trigger a change in the <c>CurrentView</c> property, setting it to <c>TemplatesVM</c>.
        /// </summary>
        public RelayCommand TemplatesViewCommand { get; }

        /// <summary>
        /// The <c>HomeVM</c> property refers to the current instance of the <c>HomeViewModel</c> and accompanying view.
        /// </summary>
        public HomeViewModel HomeVM;
        /// <summary>
        /// The <c>NewVM</c> property refers to the current instance of the <c>NewViewModel</c> and accompanying view.
        /// </summary>
        public NewViewModel NewVM;
        /// <summary>
        /// The <c>OpenVM</c> property refers to the current instance of the <c>OpenViewModel</c> and accompanying view.
        /// </summary>
        public OpenViewModel OpenVM;
        /// <summary>
        /// The <c>PinnedVM</c> property refers to the current instance of the <c>PinnedViewModel</c> and accompanying view.
        /// </summary>
        public PinnedViewModel PinnedVM;
        /// <summary>
        /// The <c>RecentVM</c> property refers to the current instance of the <c>RecentViewModel</c> and accompanying view.
        /// </summary>
        public RecentViewModel RecentVM;
        /// <summary>
        /// The <c>SettingsVM</c> property refers to the current instance of the <c>SettingsViewModel</c> and accompanying view.
        /// </summary>
        public SettingsViewModel SettingsVM;
        /// <summary>
        /// The <c>TemplatesVM</c> property refers to the current instance of the <c>TemplatesViewModel</c> and accompanying view.
        /// </summary>
        public TemplatesViewModel TemplatesVM;

        /// <summary>
        /// The <c>HomeIsChecked</c> property exposes the 'IsChecked' property of the Home button, so that it may be set in response to <c>ChangeViewMessage</c> events.
        /// </summary>
        private bool _homeIsChecked;
        public bool HomeIsChecked { get => _homeIsChecked; set => SetProperty(ref _homeIsChecked, value); }
        /// <summary>
        /// The <c>NewIsChecked</c> property exposes the 'IsChecked' property of the New button, so that it may be set in response to <c>ChangeViewMessage</c> events.
        /// </summary>
        private bool _newIsChecked;
        public bool NewIsChecked { get => _newIsChecked; set => SetProperty(ref _newIsChecked, value); }
        /// <summary>
        /// The <c>OpenIsChecked</c> property exposes the 'IsChecked' property of the Open button, so that it may be set in response to <c>ChangeViewMessage</c> events.
        /// </summary>
        private bool _openIsChecked;
        public bool OpenIsChecked { get => _openIsChecked; set => SetProperty(ref _openIsChecked, value); }
        /// <summary>
        /// The <c>PinnedIsChecked</c> property exposes the 'IsChecked' property of the Pinned button, so that it may be set in response to <c>ChangeViewMessage</c> events.
        /// </summary>
        private bool _pinnedIsChecked;
        public bool PinnedIsChecked { get => _pinnedIsChecked; set => SetProperty(ref _pinnedIsChecked, value); }
        /// <summary>
        /// The <c>RecentIsChecked</c> property exposes the 'IsChecked' property of the Recent button, so that it may be set in response to <c>ChangeViewMessage</c> events.
        /// </summary>
        private bool _recentIsChecked;
        public bool RecentIsChecked { get => _recentIsChecked; set => SetProperty(ref _recentIsChecked, value); }

        /// <summary>
        /// The <c>CurrentView</c> property exposes the currently assigned ViewModel to the program, linking to the View to display.
        /// </summary>
        private object _currentView;
        public object CurrentView { get => _currentView; set => SetProperty(ref _currentView, value); }

        /// <summary>
        /// A constructor for <c>MainViewModel</c> that assigns default values to all class variables.
        /// </summary>
        public MainViewModel() 
        {
            // Activate MainViewModel to receive messages.
            IsActive = true;

            // Set the Logo URI Resolver, a fancy little solution that can automatically return a relevant URI depending on the theme selected, or just a backup if the main one can't be resolved.
            VOWsuiteLogo = new("pack://application:,,,/Resources/Images/VOWsuite-logos_white.png",
                "pack://application:,,,/Resources/Images/VOWsuite-logos_black.png",
                new Uri("/Resources/Images/VOWsuite-logos_black.png", UriKind.Relative));

            // Assign function for WinNav commands.
            WinNavMinimiseCommand = new(() =>
            {
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            });
            WinNavCloseCommand = new(() =>
            {
                Application.Current.Shutdown();
            });

            // Assign values for Home view.
            HomeVM = new();
            HomeViewCommand = new(() => 
            { 
                CurrentView = HomeVM; 
            });
            // Assign values for New view.
            NewVM = new();
            NewViewCommand = new(() => 
            { 
                CurrentView = NewVM; 
            });
            // Assign values for Open view.
            OpenVM = new();
            OpenViewCommand = new(() => 
            { 
                CurrentView = OpenVM; 
            });
            // Assign values for Pinned view.
            PinnedVM = new();
            PinnedViewCommand = new(() => 
            { 
                CurrentView = PinnedVM; 
            });
            // Assign values for Recent view.
            RecentVM = new();
            RecentViewCommand = new(() =>
            {
                CurrentView = RecentVM;
            });
            // Assign values for Settings view.
            SettingsVM = new();
            SettingsViewCommand = new(() =>
            {
                if (CurrentView == SettingsVM)
                {
                    CurrentView = HomeVM;
                    UpdateMenuButtonSelection(0);
                }
                else
                {
                    CurrentView = SettingsVM;
                    UpdateMenuButtonSelection(5);
                }
            });
            // Assign values for Templates view.
            TemplatesVM = new();
            TemplatesViewCommand = new(() =>
            {
                CurrentView = TemplatesVM;
            });

            // Set CurrentView to default menu (HomeVM).
            CurrentView = HomeVM;

            // Update menu buttons.
            UpdateMenuButtonSelection(0);
        }

        /// <summary>
        /// The overriden <c>OnActivated</c> method registers the class with a variety of messagers from the Messenger object.
        /// </summary>
        protected override void OnActivated()
        {
            // Register the class to receive ChangeViewMessage events.
            Messenger.Register<MainViewModel, ChangeViewMessage>(this, (r, m) => r.Receive(m));
        }

        /// <summary>
        /// The <c>Receive</c> method will be called whenever the <c>ChangeViewMessage</c> is sent.
        /// </summary>
        /// <param name="message">The event that was sent, with data.</param>
        private void Receive(ChangeViewMessage message)
        {
            if (message.IsExplicit)
            {
                CurrentView = GetViewModel(message.Id);
                UpdateMenuButtonSelection(message.Id);
                return;
            }
            // Fetch both viewmodels.
            object vm1 = GetViewModel(message.Id);
            object vm2 = GetViewModel((int)message.OtherId);
            // Attempt to toggle them.
            if (CurrentView == vm1)
            {
                CurrentView = vm2;
                UpdateMenuButtonSelection((int)message.OtherId);
            }
            else if (CurrentView == vm2)
            {
                CurrentView = vm1;
                UpdateMenuButtonSelection(message.Id);
            }
        }

        /// <summary>
        /// The <c>GetViewModel</c> method converts an Id to it's corresponding ViewModel.
        /// </summary>
        /// <param name="id">The Id of the ViewModel to fetch.</param>
        /// <returns>The ViewModel</returns>
        private object GetViewModel(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return id switch
            {
                0 => HomeVM,
                1 => NewVM,
                2 => OpenVM,
                3 => PinnedVM,
                4 => RecentVM,
                5 => SettingsVM,
                6 => TemplatesVM,
                _ => null,
            };;
#pragma warning restore CS8603 // Possible null reference return.
        }
    
        /// <summary>
        /// The <c>UpdateMenuButtonSelection</c> method will update the IsChecked property of all menu buttons to ensure the correct one is selected.
        /// </summary>
        /// <param name="vmId">The Id of the ViewModel.</param>
        private void UpdateMenuButtonSelection(int vmId)
        {
            HomeIsChecked = vmId == 0;
            NewIsChecked = vmId == 1 || vmId == 6; // New or Templates view.
            OpenIsChecked = vmId == 2;
            PinnedIsChecked = vmId == 3;
            RecentIsChecked = vmId == 4;
        }
    }
}
