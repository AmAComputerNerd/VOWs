using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows;
using VOWs.MVVM.Model;

namespace VOWs.MVVM.ViewModel
{
    public class DocumentEditViewModel : ObservableObject
    {
        // Copies of global resources relevant to the DocumentEditView.
        /// <summary>
        /// The <c>Globals</c> property refers to an object full of shared variables across multiple classes.
        /// These may be read-only, or be able to be edited across classes. The <c>Globals</c> class is an <c>ObservableObject</c>.
        /// </summary>
        public Globals Globals { get => Globals.Default; }

        #region Binding Properties & Fields
        private bool _vows_Selected;
        private bool _tabs_HomeSelected;
        private bool _tabs_TextSelected;
        private bool _tabs_MediaSelected;
        private bool _tabs_PageSelected;
        private bool _tabs_ViewSelected;
        private bool _tabs_VersionControlSelected;
        private bool _tabs_ImageSelected;
        private bool _tabs_VideoSelected;
        private Visibility _sideBar;
        private Visibility _tabs_Image;
        private Visibility _tabs_Video;
        private Visibility _content;
        private Visibility _content_Home;
        private Visibility _content_Text;
        private Visibility _content_Media;
        private Visibility _content_Page;
        private Visibility _content_View;
        private Visibility _content_VersionControl;
        private Visibility _content_Image;
        private Visibility _content_Video;

        /// <summary>
        /// The <c>VOWs_Selected</c> property refers to whether the VOWs button is currently toggled on or off.
        /// This, in turn, will toggle the expanding side bar to the left of the display.
        /// </summary>
        public bool VOWs_Selected
        {
            get => _vows_Selected;
            set
            {
                // Set the accompanying SideBar variable to either 'Visibility.Visible' or 'Visibility.Collapsed'
                SideBar = value ? Visibility.Visible : Visibility.Collapsed;
                // Set the property to the new value.
                SetProperty(ref _vows_Selected, value);
            }
        }
        /// <summary>
        /// The <c>Tabs_HomeSelected</c> property refers to whether the "Home" tab is currently selected.
        /// </summary>
        public bool Tabs_HomeSelected
        {
            get => _tabs_HomeSelected;
            set
            {
                // If the new value and existing value are both already true, we should dock the Content bar.
                if (value && Tabs_HomeSelected)
                {
                    // Set the Content panel visibility to 'Visibility.Collapsed'.
                    Content = Visibility.Collapsed;
#pragma warning disable CA2011 // Avoid infinite recursion
                    Tabs_HomeSelected = false;
#pragma warning restore CA2011 // Avoid infinite recursion
                    return;
                }
                else if (value)
                {
                    // Set the Content panel visibility to 'Visibility.Visible', in case it is still collapsed.
                    if (Content == Visibility.Collapsed) Content = Visibility.Visible;
                    // Set other properties to false.
                    Tabs_TextSelected = false;
                    Tabs_MediaSelected = false;
                    Tabs_PageSelected = false;
                    Tabs_ViewSelected = false;
                    Tabs_VersionControlSelected = false;
                    Tabs_ImageSelected = false;
                    Tabs_VideoSelected = false;
                }
                // Set the accompanying Content_Home variable to either 'Visibility.Visible' or 'Visibility.Collapsed'.
                Content_Home = value ? Visibility.Visible : Visibility.Collapsed;
                // Set the property to the new value.
                SetProperty(ref _tabs_HomeSelected, value);
            }
        }
        /// <summary>
        /// The <c>Tabs_TextSelected</c> property refers to whether the "Text" tab is currently selected.
        /// </summary>
        public bool Tabs_TextSelected
        {
            get => _tabs_TextSelected;
            set
            {
                // If the new value and existing value are both already true, we should dock the Content bar.
                if (value && Tabs_TextSelected)
                {
                    // Set the Content panel visibility to 'Visibility.Collapsed'.
                    Content = Visibility.Collapsed;
                    // Now that Content is false, we'll use this variable to set the property back to false.
#pragma warning disable CA2011 // Avoid infinite recursion
                    Tabs_TextSelected = false;
#pragma warning restore CA2011 // Avoid infinite recursion
                    return;
                }
                else if (value)
                {
                    // Set the Content panel visibility to 'Visibility.Visible', in case it is still collapsed.
                    if (Content == Visibility.Collapsed) Content = Visibility.Visible;
                    // Set other properties to false.
                    Tabs_HomeSelected = false;
                    Tabs_MediaSelected = false;
                    Tabs_PageSelected = false;
                    Tabs_ViewSelected = false;
                    Tabs_VersionControlSelected = false;
                    Tabs_ImageSelected = false;
                    Tabs_VideoSelected = false;
                }
                // Set the accompanying Content_Text variable to either 'Visibility.Visible' or 'Visibility.Collapsed'.
                Content_Text = value ? Visibility.Visible : Visibility.Collapsed;
                // Set the property to the new value.
                SetProperty(ref _tabs_TextSelected, value);
            }
        }
        /// <summary>
        /// The <c>Tabs_MediaSelected</c> property refers to whether the "Media" tab is currently selected.
        /// </summary>
        public bool Tabs_MediaSelected
        {
            get => _tabs_MediaSelected;
            set
            {
                // If the new value and existing value are both already true, we should dock the Content bar.
                if (value && Tabs_MediaSelected)
                {
                    // Set the Content panel visibility to 'Visibility.Collapsed'.
                    Content = Visibility.Collapsed;
                    // Now that Content is false, we'll use this variable to set the property back to false.
#pragma warning disable CA2011 // Avoid infinite recursion
                    Tabs_MediaSelected = false;
#pragma warning restore CA2011 // Avoid infinite recursion
                    return;
                }
                else if (value)
                {
                    // Set the Content panel visibility to 'Visibility.Visible', in case it is still collapsed.
                    if (Content == Visibility.Collapsed) Content = Visibility.Visible;
                    // Set other properties to false.
                    Tabs_HomeSelected = false;
                    Tabs_TextSelected = false;
                    Tabs_PageSelected = false;
                    Tabs_ViewSelected = false;
                    Tabs_VersionControlSelected = false;
                    Tabs_ImageSelected = false;
                    Tabs_VideoSelected = false;
                }
                // Set the accompanying Content_Media variable to either 'Visibility.Visible' or 'Visibility.Collapsed'.
                Content_Media = value ? Visibility.Visible : Visibility.Collapsed;
                // Set the property to the new value.
                SetProperty(ref _tabs_MediaSelected, value);
            }
        }
        /// <summary>
        /// The <c>Tabs_PageSelected</c> property refers to whether the "Page" tab is currently selected.
        /// </summary>
        public bool Tabs_PageSelected
        {
            get => _tabs_PageSelected;
            set
            {
                // If the new value and existing value are both already true, we should dock the Content bar.
                if (value && Tabs_PageSelected)
                {
                    // Set the Content panel visibility to 'Visibility.Collapsed'.
                    Content = Visibility.Collapsed;
                    // Now that Content is false, we'll use this variable to set the property back to false.
#pragma warning disable CA2011 // Avoid infinite recursion
                    Tabs_PageSelected = false;
#pragma warning restore CA2011 // Avoid infinite recursion
                    return;
                }
                else if (value)
                {
                    // Set the Content panel visibility to 'Visibility.Visible', in case it is still collapsed.
                    if (Content == Visibility.Collapsed) Content = Visibility.Visible;
                    // Set other properties to false.
                    Tabs_HomeSelected = false;
                    Tabs_TextSelected = false;
                    Tabs_MediaSelected = false;
                    Tabs_ViewSelected = false;
                    Tabs_VersionControlSelected = false;
                    Tabs_ImageSelected = false;
                    Tabs_VideoSelected = false;
                }
                // Set the accompanying Content_Home variable to either 'Visibility.Visible' or 'Visibility.Collapsed'.
                Content_Page = value ? Visibility.Visible : Visibility.Collapsed;
                // Set the property to the new value.
                SetProperty(ref _tabs_PageSelected, value);
            }
        }
        /// <summary>
        /// The <c>Tabs_ViewSelected</c> property refers to whether the "View" tab is currently selected.
        /// </summary>
        public bool Tabs_ViewSelected
        {
            get => _tabs_ViewSelected;
            set
            {
                // If the new value and existing value are both already true, we should dock the Content bar.
                if (value && Tabs_ViewSelected)
                {
                    // Set the Content panel visibility to 'Visibility.Collapsed'.
                    Content = Visibility.Collapsed;
                    // Now that Content is false, we'll use this variable to set the property back to false.
#pragma warning disable CA2011 // Avoid infinite recursion
                    Tabs_ViewSelected = false;
#pragma warning restore CA2011 // Avoid infinite recursion
                    return;
                }
                else if (value)
                {
                    // Set the Content panel visibility to 'Visibility.Visible', in case it is still collapsed.
                    if (Content == Visibility.Collapsed) Content = Visibility.Visible;
                    // Set other properties to false.
                    Tabs_HomeSelected = false;
                    Tabs_TextSelected = false;
                    Tabs_MediaSelected = false;
                    Tabs_PageSelected = false;
                    Tabs_VersionControlSelected = false;
                    Tabs_ImageSelected = false;
                    Tabs_VideoSelected = false;
                }
                // Set the accompanying Content_View variable to either 'Visibility.Visible' or 'Visibility.Collapsed'.
                Content_View = value ? Visibility.Visible : Visibility.Collapsed;
                // Set the property to the new value.
                SetProperty(ref _tabs_ViewSelected, value);
            }
        }
        /// <summary>
        /// The <c>Tabs_VersionControlSelected</c> property refers to whether the "Version Control" tab is currently selected.
        /// </summary>
        public bool Tabs_VersionControlSelected
        {
            get => _tabs_VersionControlSelected;
            set
            {
                // If the new value and existing value are both already true, we should dock the Content bar.
                if (value && Tabs_VersionControlSelected)
                {
                    // Set the Content panel visibility to 'Visibility.Collapsed'.
                    Content = Visibility.Collapsed;
                    // Now that Content is false, we'll use this variable to set the property back to false.
#pragma warning disable CA2011 // Avoid infinite recursion
                    Tabs_VersionControlSelected = false;
#pragma warning restore CA2011 // Avoid infinite recursion
                    return;
                }
                else if (value)
                {
                    // Set the Content panel visibility to 'Visibility.Visible', in case it is still collapsed.
                    if (Content == Visibility.Collapsed) Content = Visibility.Visible;
                    // Set other properties to false.
                    Tabs_HomeSelected = false;
                    Tabs_TextSelected = false;
                    Tabs_MediaSelected = false;
                    Tabs_PageSelected = false;
                    Tabs_ViewSelected = false;
                    Tabs_ImageSelected = false;
                    Tabs_VideoSelected = false;
                }
                // Set the accompanying Content_VersionControl variable to either 'Visibility.Visible' or 'Visibility.Collapsed'.
                Content_VersionControl = value ? Visibility.Visible : Visibility.Collapsed;
                // Set the property to the new value.
                SetProperty(ref _tabs_VersionControlSelected, value);
            }
        }
        /// <summary>
        /// The <c>Tabs_ImageSelected</c> property refers to whether the "Image" tab is currently selected.
        /// </summary>
        public bool Tabs_ImageSelected
        {
            get => _tabs_ImageSelected;
            set
            {
                // If the new value and existing value are both already true, we should dock the Content bar.
                if (value && Tabs_ImageSelected)
                {
                    // Set the Content panel visibility to 'Visibility.Collapsed'.
                    Content = Visibility.Collapsed;
                    // Now that Content is false, we'll use this variable to set the property back to false.
#pragma warning disable CA2011 // Avoid infinite recursion
                    Tabs_ImageSelected = false;
#pragma warning restore CA2011 // Avoid infinite recursion
                    return;
                }
                else if (value)
                {
                    // Set the Content panel visibility to 'Visibility.Visible', in case it is still collapsed.
                    if (Content == Visibility.Collapsed) Content = Visibility.Visible;
                    // Set other properties to false.
                    Tabs_HomeSelected = false;
                    Tabs_TextSelected = false;
                    Tabs_MediaSelected = false;
                    Tabs_PageSelected = false;
                    Tabs_ViewSelected = false;
                    Tabs_VersionControlSelected = false;
                    Tabs_VideoSelected = false;
                }
                // Set the accompanying Content_Image variable to either 'Visibility.Visible' or 'Visibility.Collapsed'.
                Content_Image = value ? Visibility.Visible : Visibility.Collapsed;
                // Set the property to the new value.
                SetProperty(ref _tabs_ImageSelected, value);
            }
        }
        /// <summary>
        /// The <c>Tabs_VideoSelected</c> property refers to whether the "Video" tab is currently selected.
        /// </summary>
        public bool Tabs_VideoSelected
        {
            get => _tabs_VideoSelected;
            set
            {
                // If the new value and existing value are both already true, we should dock the Content bar.
                if (value && Tabs_VideoSelected)
                {
                    // Set the Content panel visibility to 'Visibility.Collapsed'.
                    Content = Visibility.Collapsed;
                    // Now that Content is false, we'll use this variable to set the property back to false.
#pragma warning disable CA2011 // Avoid infinite recursion
                    Tabs_VideoSelected = false;
#pragma warning restore CA2011 // Avoid infinite recursion
                    return;
                }
                else if (value)
                {
                    // Set the Content panel visibility to 'Visibility.Visible', in case it is still collapsed.
                    if (Content == Visibility.Collapsed) Content = Visibility.Visible;
                    // Set other properties to false.
                    Tabs_HomeSelected = false;
                    Tabs_TextSelected = false;
                    Tabs_MediaSelected = false;
                    Tabs_PageSelected = false;
                    Tabs_ViewSelected = false;
                    Tabs_VersionControlSelected = false;
                    Tabs_ImageSelected = false;
                }
                // Set the accompanying Content_Video variable to either 'Visibility.Visible' or 'Visibility.Collapsed'.
                Content_Video = value ? Visibility.Visible : Visibility.Collapsed;
                // Set the property to the new value.
                SetProperty(ref _tabs_VideoSelected, value);
            }
        }
        /// <summary>
        /// The <c>SideBar</c> property refers to whether the "Side bar", containing various global actions, is visible (expanded) or not.
        /// </summary>
        public Visibility SideBar { get => _sideBar; set => SetProperty(ref _sideBar, value); }
        /// <summary>
        /// The <c>Tabs_VersionControl</c> property refers to whether the "Version Control" tab should be visible as a category.
        /// It is uniquely linked to application settings, as the user has a choice on whether Version Control options are shown along other categories or in a standalone dockable menu.
        /// </summary>
        public Visibility Tabs_VersionControl { get => Globals.UseTabsForVersionControl ? Visibility.Visible : Visibility.Collapsed; }
        /// <summary>
        /// The <c>Tabs_Image</c> property refers to whether the "Image" special tab should be visible as a category.
        /// The functionality behind this property isn't implemented yet. It's future use should be to become visible whenever an Image object is selected.
        /// </summary>
        public Visibility Tabs_Image { get => _tabs_Image; set => SetProperty(ref _tabs_Image, value); }
        /// <summary>
        /// The <c>Tabs_Video</c> property refers to whether the "Video" special tab should be visible as a category.
        /// The functionality behind this property isn't implemented yet. It's future use should be to become visible whenever a Video object is selected.
        /// </summary>
        public Visibility Tabs_Video { get => _tabs_Video; set => SetProperty(ref _tabs_Video, value); }
        /// <summary>
        /// The <c>Content</c> property refers to whether the Content menu is docked or not.
        /// When a user clicks on a category button for a second time after it has been selected, this property is set to <c>Visibility.Collapsed</c>.
        /// When a user clicks on any category button while this property is set to <c>Visibility.Collapsed</c>, this property will be set to <c>Visibility.Visible</c>.
        /// </summary>
        public Visibility Content { get => _content; set => SetProperty(ref _content, value); }
        /// <summary>
        /// The <c>Content_Home</c> property refers to whether the Content page for the 'Home' category should be shown.
        /// This property will be toggled as needed when a user interacts with the category buttons.
        /// </summary>
        public Visibility Content_Home { get => _content_Home; set => SetProperty(ref _content_Home, value); }
        /// <summary>
        /// The <c>Content_Text</c> property refers to whether the Content page for the 'Text' category should be shown.
        /// This property will be toggled as needed when a user interacts with the category buttons.
        /// </summary>
        public Visibility Content_Text { get => _content_Text; set => SetProperty(ref _content_Text, value); }
        /// <summary>
        /// The <c>Content_Media</c> property refers to whether the Content page for the 'Media' category should be shown.
        /// This property will be toggled as needed when a user interacts with the category buttons.
        /// </summary>
        public Visibility Content_Media { get => _content_Media; set => SetProperty(ref _content_Media, value); }
        /// <summary>
        /// The <c>Content_Page</c> property refers to whether the Content page for the 'Page' category should be shown.
        /// This property will be toggled as needed when a user interacts with the category buttons.
        /// </summary>
        public Visibility Content_Page { get => _content_Page; set => SetProperty(ref _content_Page, value); }
        /// <summary>
        /// The <c>Content_View</c> property refers to whether the Content page for the 'View' category should be shown.
        /// This property will be toggled as needed when a user interacts with the category buttons.
        /// </summary>
        public Visibility Content_View { get => _content_View; set => SetProperty(ref _content_View, value); }
        /// <summary>
        /// The <c>Content_VersionControl</c> property refers to whether the Content page for the 'Version Control' category should be shown.
        /// This property will be toggled as needed when a user interacts with the category buttons.
        /// If <c>Globals.UseTabsForVersionControl</c> is <c>false</c>, this property will always be <c>Visibility.Collapsed</c>.
        /// </summary>
        public Visibility Content_VersionControl { get => _content_VersionControl; set => SetProperty(ref _content_VersionControl, value); }
        /// <summary>
        /// The <c>Content_Image</c> property refers to whether the Content page for the 'Image' special category should be shown.
        /// This property will be toggled as needed when a user interacts with the category buttons.
        /// When <c>Tabs_Image</c> is changed to <c>Visibility.Collapsed</c>, this property will also change to <c>Visibility.Collapsed</c> and <c>Content_Home</c> will
        /// be set to <c>Visibility.Visible</c>.
        /// </summary>
        public Visibility Content_Image { get => _content_Image; set => SetProperty(ref _content_Image, value); }
        /// <summary>
        /// The <c>Content_Video</c> property refers to whether the Content page for the 'Video' special category should be shown.
        /// This property will be toggled as needed when a user interacts with the category buttons.
        /// When <c>Tabs_Video</c> is changed to <c>Visibility.Collapsed</c>, this property will also change to <c>Visibility.Collapsed</c> and <c>Content_Home</c> will
        /// be set to <c>Visibility.Visible</c>.
        /// </summary>
        public Visibility Content_Video { get => _content_Video; set => SetProperty(ref _content_Video, value); }
        #endregion
        #region Commands
        public RelayCommand ToggleDockableSideMenuCommand;
        #endregion
        #region Other Properties & Fields
        /// <summary>
        /// The <c>PageVM</c> property refers to the instance of the Page View and ViewModel.
        /// It holds data about the current open document, as well as handling display and related issues.
        /// </summary>
        public PageViewModel PageVM { get; }
        /// <summary>
        /// The <c>Logo</c> property refers to a theme-dependent Uri or ImageSource for the VOWsuite logo.
        /// This logo is the simple (~1:1) version of the logo.
        /// </summary>
        public DynamicURIResolver Logo { get; }
        #endregion

        public DocumentEditViewModel()
        {
            #region Set "Binding Properties & Fields" region variables.
            // This is temporary - later, this will be done automatically.
            Tabs_Image = Visibility.Visible;
            Tabs_Video = Visibility.Collapsed;
            Content = Visibility.Visible;
            Tabs_HomeSelected = true;
            VOWs_Selected = false;
            #endregion
            #region Set "Commands" region variables.
            ToggleDockableSideMenuCommand = new(() =>
            {
                // TODO: Add DockableSideMenu binding properties.
            });
            #endregion
            #region Set "Other Properties & Fields" region variables.
            Logo = new("pack://application:,,,/Resources/Images/VOWsuite-logos_white.png",
                "pack://application:,,,/Resources/Images/VOWsuite-logos_black.png",
                new Uri("/Resources/Images/VOWsuite-logos_black.png",
                UriKind.Relative));
            PageVM = new PageViewModel();
            #endregion
        }
    }
}
