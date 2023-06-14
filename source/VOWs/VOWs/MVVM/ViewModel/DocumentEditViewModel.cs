using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VOWs.Events;
using VOWs.MVVM.Model;

namespace VOWs.MVVM.ViewModel
{
    public partial class DocumentEditViewModel : ObservableRecipient
    {
        // Copies of global resources relevant to the DocumentEditView.
        /// <summary>
        /// The <c>_storage</c> private parameter stores the backup value for <c>Storage</c>.
        /// </summary>
        private DatabaseWrapper _storage;
        /// <summary>
        /// The <c>Storage</c> parameter links back to the <c>MainViewModel</c>'s Storage parameter, and is
        /// either retrieved through messages or using the backup <c>_storage</c> parameter.
        /// </summary>
        public DatabaseWrapper Storage
        {
            get
            {
                try
                {
                    DatabaseWrapper retrievedWrapper = Messenger.Send(new RequestStorageMessage());
                    SetProperty(ref _storage, retrievedWrapper);
                }
                catch (Exception)
                {
                    // RequestStorageMessage was never answered (likely due to being called at startup).
                    // We'll return our saved copy of _storage in this case.
                }
                return _storage;
            }
            set
            {
                // Send the UpdateStorageMessage and save the new value into _storage.
                bool updated = Messenger.Send(new UpdateStorageMessage(value));
                // If Storage was never updated, we shouldn't update our local variable here.
                if (updated) SetProperty(ref _storage, value);
                else throw new ArgumentException("Storage variable was unable to be updated.");
            }
        }
        /// <summary>
        /// The <c>EnvironmentArgs</c> parameter is a copy of the Environment arguments, also known as command
        /// line arguments. This is constantly recycled to ensure that data remains relatively read-only, while
        /// also allowing classes to use instances of the object to store modified values.
        /// </summary>
        public EnvironmentArgs EnvironmentArgs { get => new EnvironmentArgs(); }

        // Local resources relevant to the DocumentEditView.
        /// <summary>
        /// The <c>_VOWsuiteLogoUriResolver</c> private parameter stores the value for <c>VOWsuiteLogoSource</c>.
        /// </summary>
        private DynamicURIResolver _VOWsuiteLogoUriResolver;
        /// <summary>
        /// The <c>VOWsuiteLogoSource</c> parameter stores an <c>ImageSource</c> to be accessed by the View.
        /// It will always resolve to the <c>ImageSource</c> most fitting of the current selected theme.
        /// </summary>
        public ImageSource VOWsuiteImageSource { get => new BitmapImage(_VOWsuiteLogoUriResolver.Uri); }

        /// <summary>
        /// The <c>SwitchTab_Home</c> command will trigger switching to the Home category, which will toggle the state of the button and unhide the Home
        /// category menu buttons.
        /// </summary>
        public RelayCommand SwitchTab_Home;
        public Visibility HomeCategoryVisibility = Visibility.Collapsed;
        /// <summary>
        /// The <c>SwitchTab_Text</c> command will trigger switching to the Text category, which will toggle the state of the button and unhide the Text
        /// category menu buttons.
        /// </summary>
        public RelayCommand SwitchTab_Text;
        public Visibility TextCategoryVisibility = Visibility.Collapsed;
        /// <summary>
        /// The <c>SwitchTab_Media</c> command will trigger switching to the Media category, which will toggle the state of the button and unhide the 
        /// Media category menu buttons.
        /// </summary>
        public RelayCommand SwitchTab_Media;
        public Visibility MediaCategoryVisibility = Visibility.Collapsed;
        /// <summary>
        /// The <c>SwitchTab_Page</c> command will trigger switching to the Page category, which will toggle the state of the button and unhide the Page
        /// category menu buttons.
        /// </summary>
        public RelayCommand SwitchTab_Page;
        public Visibility PageCategoryVisibility = Visibility.Collapsed;
        /// <summary>
        /// The <c>SwitchTab_View</c> command will trigger switching to the View category, which will toggle the state of the button and unhide the View
        /// category menu buttons.
        /// </summary>
        public RelayCommand SwitchTab_View;
        public Visibility ViewCategoryVisibility = Visibility.Collapsed;
        /// <summary>
        /// The <c>SwitchTab_VersionControl</c> command will trigger switching to the Version Control category, which will toggle the state of the button 
        /// and unhide the Version Control category menu buttons.
        /// </summary>
        public RelayCommand SwitchTab_VersionControl;
        public Visibility VersionControlCategoryVisibility = Visibility.Collapsed;

        /// <summary>
        /// The <c>VOWsuiteButtonCommand</c> parameter stores a <c>RelayCommand</c> that will trigger the
        /// MainViewModel to change the <c>CurrentView</c> to <c>SettingsView</c>.
        /// </summary>
        public RelayCommand VOWsuiteButtonCommand { get; }
        /// <summary>
        /// The <c>PageVM</c> parameter stores the <c>PageViewModel</c> object, and is fed information about
        /// the open document through messages.
        /// </summary>
        public PageViewModel PageVM { get; }

        /// <summary>
        /// The constructor for <c>DocumentEditViewModel</c> intialises variables relevant to the
        /// <c>DocumentEditView</c>.
        /// </summary>
        public DocumentEditViewModel()
        {
            // Set the backup storage variable.
            _storage = Messenger.Send(new RequestStorageMessage());
            // Set the Logo URI Resolver, a fancy little solution that can automatically return a relevant URI depending on the theme selected, or just a backup if the main one can't be resolved.
            string _whiteLogoSource = "pack://application:,,,/Resources/Images/VOWsuite-logos_white.png";
            string _blackLogoSource = "pack://application:,,,/Resources/Images/VOWsuite-logos_black.png";
            _VOWsuiteLogoUriResolver = new(_whiteLogoSource, _blackLogoSource, new Uri("Resources/Images/VOWsuite-logos_white.png", UriKind.Relative));
            // Set VOWsuiteButtonCommand to send a message when clicked.
            VOWsuiteButtonCommand = new(() =>
            {
                // Send a ChangeViewMessage message to the MainViewModel.
                Messenger.Send(new ChangeViewMessage(0, 1));
            });
            // Set the various category change buttons.
            SwitchTab_Home = new(() =>
            {
                // Hide other tabs.
                TextCategoryVisibility = Visibility.Collapsed;
                MediaCategoryVisibility = Visibility.Collapsed;
                PageCategoryVisibility = Visibility.Collapsed;
                ViewCategoryVisibility = Visibility.Collapsed;
                VersionControlCategoryVisibility = Visibility.Collapsed;
                // Show Home.
                HomeCategoryVisibility = Visibility.Visible;
            });
            SwitchTab_Text = new(() =>
            {
                // Hide other tabs.
                HomeCategoryVisibility = Visibility.Collapsed;
                MediaCategoryVisibility = Visibility.Collapsed;
                PageCategoryVisibility = Visibility.Collapsed;
                ViewCategoryVisibility = Visibility.Collapsed;
                VersionControlCategoryVisibility = Visibility.Collapsed;
                // Show Text.
                TextCategoryVisibility = Visibility.Visible;
            });
            SwitchTab_Media = new(() =>
            {
                // Hide other tabs.
                HomeCategoryVisibility = Visibility.Collapsed;
                TextCategoryVisibility = Visibility.Collapsed;
                PageCategoryVisibility = Visibility.Collapsed;
                ViewCategoryVisibility = Visibility.Collapsed;
                VersionControlCategoryVisibility = Visibility.Collapsed;
                // Show Media.
                MediaCategoryVisibility = Visibility.Visible;
            });
            SwitchTab_Page = new(() =>
            {
                // Hide other tabs.
                HomeCategoryVisibility = Visibility.Collapsed;
                TextCategoryVisibility = Visibility.Collapsed;
                MediaCategoryVisibility = Visibility.Collapsed;
                ViewCategoryVisibility = Visibility.Collapsed;
                VersionControlCategoryVisibility = Visibility.Collapsed;
                // Show Page.
                PageCategoryVisibility = Visibility.Visible;
            });
            SwitchTab_View = new(() =>
            {
                // Hide other tabs.
                HomeCategoryVisibility = Visibility.Collapsed;
                TextCategoryVisibility = Visibility.Collapsed;
                MediaCategoryVisibility = Visibility.Collapsed;
                PageCategoryVisibility = Visibility.Collapsed;
                VersionControlCategoryVisibility = Visibility.Collapsed;
                // Show View.
                ViewCategoryVisibility = Visibility.Visible;
            });
            SwitchTab_VersionControl = new(() =>
            {
                // Hide other tabs.
                HomeCategoryVisibility = Visibility.Collapsed;
                TextCategoryVisibility = Visibility.Collapsed;
                MediaCategoryVisibility = Visibility.Collapsed;
                PageCategoryVisibility = Visibility.Collapsed;
                ViewCategoryVisibility = Visibility.Collapsed;
                // Show Version Control.
                VersionControlCategoryVisibility = Visibility.Visible;
            });
            // Set the Page ViewModel.
            PageVM = new PageViewModel();
        }
    }
}
