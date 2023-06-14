using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Windows;
using VOWs.Events;
using VOWs.MVVM.Model;

namespace VOWs.MVVM.ViewModel
{
    public partial class DocumentEditViewModel : ObservableRecipient
    {
        // Copies of global resources relevant to the DocumentEditView.
        /// <summary>
        /// The <c>Globals</c> property refers to an object full of shared variables across multiple classes.
        /// These may be read-only, or be able to be edited across classes. The <c>Globals</c> class is an <c>ObservableObject</c>.
        /// </summary>
        public Globals Globals { get => Globals.Default; }

        // Local resources relevant to the DocumentEditView.
        /// <summary>
        /// The <c>_homeTabVisible</c> field is the storage variable for <c>HomeTabVisible</c>.
        /// </summary>
        private Visibility _homeTabVisible;
        /// <summary>
        /// The <c>_textTabVisible</c> field is the storage variable for <c>TextTabVisible</c>.
        /// </summary>
        private Visibility _textTabVisible;
        /// <summary>
        /// The <c>_mediaTabVisible</c> field is the storage variable for <c>MediaTabVisible</c>.
        /// </summary>
        private Visibility _mediaTabVisible;
        /// <summary>
        /// The <c>_pageTabVisible</c> field is the storage variable for <c>PageTabVisible</c>.
        /// </summary>
        private Visibility _pageTabVisible;
        /// <summary>
        /// The <c>_viewTabVisible</c> field is the storage variable for <c>ViewTabVisible</c>.
        /// </summary>
        private Visibility _viewTabVisible;
        /// <summary>
        /// The <c>_versionControlTabVisible</c> field is the storage variable for <c>VersionControlTabVisible</c>.
        /// </summary>
        private Visibility _versionControlTabVisible;

        /// <summary>
        /// The <c>VOWsuiteLogo</c> property is a <c>DynamicURIResolver</c> holding themed URIs for the VOWsuite logo.
        /// </summary>
        public DynamicURIResolver VOWsuiteLogo { get; }
        /// <summary>
        /// The <c>HomeTabVisible</c> property holds information about the visibility of the Home tab for the View.
        /// </summary>
        public Visibility HomeTabVisible { get => _homeTabVisible; set => SetProperty(ref _homeTabVisible, value); }
        /// <summary>
        /// The <c>TextTabVisible</c> property holds information about the visibility of the Text tab for the View.
        /// </summary>
        public Visibility TextTabVisible { get => _textTabVisible; set => SetProperty(ref _textTabVisible, value); }
        /// <summary>
        /// The <c>MediaTabVisible</c> property holds information about the visibility of the Media tab for the View.
        /// </summary>
        public Visibility MediaTabVisible { get => _mediaTabVisible; set => SetProperty(ref _mediaTabVisible, value); }
        /// <summary>
        /// The <c>PageTabVisible</c> property holds information about the visibility of the Page tab for the View.
        /// </summary>
        public Visibility PageTabVisible { get => _pageTabVisible; set => SetProperty(ref _pageTabVisible, value); }
        /// <summary>
        /// The <c>ViewTabVisible</c> property holds information about the visibility of the View tab for the View.
        /// </summary>
        public Visibility ViewTabVisible { get => _viewTabVisible; set => SetProperty(ref _viewTabVisible, value); }
        /// <summary>
        /// The <c>VersionControlTabVisible</c> property holds information about the visibility of the Version Control tab for the View.
        /// </summary>
        public Visibility VersionControlTabVisible { get => _versionControlTabVisible; set => SetProperty(ref _versionControlTabVisible, value); }
        /// <summary>
        /// The <c>VOWsuiteLogoCommand</c> property holds the RelayCommand to be executed when the VOWsuite logo is clicked.
        /// </summary>
        public RelayCommand VOWsuiteLogoCommand { get; }
        /// <summary>
        /// The <c>ToggleHomeTabCommand</c> property holds the RelayCommand to be executed when the Home button is clicked.
        /// </summary>
        public RelayCommand ToggleHomeTabCommand { get; }
        /// <summary>
        /// The <c>ToggleTextTabCommand</c> property holds the RelayCommand to be executed when the Text button is clicked.
        /// </summary>
        public RelayCommand ToggleTextTabCommand { get; }
        /// <summary>
        /// The <c>ToggleMediaTabCommand</c> property holds the RelayCommand to be executed when the Media button is clicked.
        /// </summary>
        public RelayCommand ToggleMediaTabCommand { get; }
        /// <summary>
        /// The <c>TogglePageTabCommand</c> property holds the RelayCommand to be executed when the Page button is clicked.
        /// </summary>
        public RelayCommand TogglePageTabCommand { get; }
        /// <summary>
        /// The <c>ToggleViewTabCommand</c> property holds the RelayCommand to be executed when the View button is clicked.
        /// </summary>
        public RelayCommand ToggleViewTabCommand { get; }
        /// <summary>
        /// The <c>ToggleVersionControlTabCommand</c> property holds the RelayCommand to be executed when the Version Control button is clicked.
        /// </summary>
        public RelayCommand ToggleVersionControlTabCommand { get; }
        /// <summary>
        /// The <c>PageVM</c> property holds the current instance of the PageViewModel.
        /// </summary>
        public PageViewModel PageVM { get; }

        /// <summary>
        /// The constructor for <c>DocumentEditViewModel</c> intialises variables relevant to the
        /// <c>DocumentEditView</c>.
        /// </summary>
        public DocumentEditViewModel()
        {
            // Set the Logo URI Resolver, a fancy little solution that can automatically return a relevant URI depending on the theme selected, or just a backup if the main one can't be resolved.
            VOWsuiteLogo = new("pack://application:,,,/Resources/Images/VOWsuite-logos_white.png", 
                "pack://application:,,,/Resources/Images/VOWsuite-logos_black.png", 
                new Uri("/Resources/Images/VOWsuite-logos_black.png", 
                UriKind.Relative));
            // Set default visibilities.
            HomeTabVisible = Visibility.Visible;
            TextTabVisible = Visibility.Collapsed;
            MediaTabVisible = Visibility.Collapsed;
            PageTabVisible = Visibility.Collapsed;
            ViewTabVisible = Visibility.Collapsed;
            VersionControlTabVisible = Visibility.Collapsed;
            // Set relay commands.
            VOWsuiteLogoCommand = new(() =>
            {
                Messenger.Send(new ChangeViewMessage(1));
            });
            ToggleHomeTabCommand = new(() =>
            {
                HideAllTabs();
                HomeTabVisible = Visibility.Visible;
            });
            ToggleTextTabCommand = new(() =>
            {
                HideAllTabs();
                TextTabVisible = Visibility.Visible;
            });
            ToggleMediaTabCommand = new(() =>
            {
                HideAllTabs();
                MediaTabVisible = Visibility.Visible;
            });
            TogglePageTabCommand = new(() => 
            {
                HideAllTabs();
                PageTabVisible = Visibility.Visible;
            });
            ToggleViewTabCommand = new(() =>
            {
                HideAllTabs();
                ViewTabVisible = Visibility.Visible;
            });
            ToggleVersionControlTabCommand = new(() =>
            {
                HideAllTabs();
                VersionControlTabVisible = Visibility.Visible;
            });
            // Set Page VM.
            PageVM = new PageViewModel();
        }

        /// <summary>
        /// The <c>HideAllTabs</c> method will set the visibility of all category tabs to <c>Visibility.Collapsed</c>.
        /// This ensures there is no overlapping content before the correct tab is set to Visible again.
        /// </summary>
        public void HideAllTabs()
        {
            HomeTabVisible = Visibility.Collapsed;
            TextTabVisible = Visibility.Collapsed;
            MediaTabVisible = Visibility.Collapsed;
            PageTabVisible = Visibility.Collapsed;
            ViewTabVisible = Visibility.Collapsed;
            VersionControlTabVisible= Visibility.Collapsed;
        }
    }
}
