using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VOWs.Events;
using VOWs.MVVM.Model;
using VOWs.Validators;

namespace VOWs.MVVM.ViewModel
{
    public class DocumentEditViewModel : ObservableRecipient
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

        public EnvironmentArgs EnvironmentArgs { get => new EnvironmentArgs(); }

        // Local resources relevant to the DocumentEditView.
        /// <summary>
        /// The <c>_vowsuiteLogoUriResolver</c> private parameter stores the value for <c>VOWsuiteLogoSource</c>.
        /// </summary>
        private DynamicURIResolver _vowsuiteLogoUriResolver;
        /// <summary>
        /// The <c>VOWsuiteLogoSource</c> parameter stores an <c>ImageSource</c> to be accessed by the View.
        /// It will always resolve to the <c>ImageSource</c> most fitting of the current selected theme.
        /// </summary>
        public ImageSource VOWsuiteLogoSource { get => new BitmapImage(_vowsuiteLogoUriResolver.Uri); }
        private DynamicURIResolver _exampleImageUriResolver;
        public ImageSource ExampleImageSource { get => new BitmapImage(_vowsuiteLogoUriResolver.Uri); }

        /// <summary>
        /// The <c>MenuContent</c> parameter stores a collection of objects that are the menu
        /// buttons / content for the currently selected category.
        /// </summary>
        public ObservableCollection<object> MenuContent { get; }
        /// <summary>
        /// The <c>CurrentFont</c> parameter stores the <c>Font</c> object that the <c>PageVM</c> should
        /// pull from when writing new text.
        /// </summary>
        public Font CurrentFont { get; }

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
            _vowsuiteLogoUriResolver = new(_whiteLogoSource, _blackLogoSource, new Uri("Resources/Images/VOWsuite-logos_white.png", UriKind.Relative));
            // Set VOWsuiteButtonCommand to send a message when clicked.
            VOWsuiteButtonCommand = new(() =>
            {
                // Send a ChangeViewMessage message to the MainViewModel.
                Messenger.Send(new ChangeViewMessage(0, 1));
            });
            // Set the MenuContent collection to a new list.
            MenuContent = new();
            // Update the MenuContent.
            //UpdateMenuContent();
            // Set the Page ViewModel.
            PageVM = new PageViewModel();
        }
    
        /// <summary>
        /// The <c>UpdateMenuContent</c> method will sequentially create all relevant menu content to a category
        /// and add them to the <c>MenuContent</c> collection.
        /// </summary>
        private void UpdateMenuContent()
        {
            // TODO: Update MenuButtons based on the selected category.
            // Create reusable temp variables.
            Image image = new Image();
            Binding binding = new Binding();
            // Retrieve styles.
            Style subpanel = (Style)Application.Current.TryFindResource("MenuPanel");
            Style bigButton = (Style)Application.Current.TryFindResource("MenuButton_Big");
            Style button = (Style)Application.Current.TryFindResource("MenuButton");
            Style bigToggleButton = (Style)Application.Current.TryFindResource("MenuToggleButton_Big");
            Style toggleButton = (Style)Application.Current.TryFindResource("MenuToggleButton");
            Style dropDownBox = (Style)Application.Current.TryFindResource("MenuDropDownBox");
            Style valueBox = (Style)Application.Current.TryFindResource("MenuValueBox");
            // Create buttons.
            // Create "Page Size" button.
            Button pageSizeButton = new Button();
            pageSizeButton.Tag = "Page Size";
            image = new Image();
            image.Source = ExampleImageSource;
            pageSizeButton.Content = image;
            pageSizeButton.Style = bigButton;
            MenuContent.Add(pageSizeButton);
            // Create "Page Orientation" button.
            Button pageOrientationButton = new Button();
            pageOrientationButton.Tag = "Page Orientation";
            image = new Image();
            image.Source = ExampleImageSource;
            pageOrientationButton.Content = image;
            pageOrientationButton.Style = bigButton;
            MenuContent.Add(pageOrientationButton);
            // Create StackPanel for font buttons.
            StackPanel fontPanel = new StackPanel();
            fontPanel.Style = subpanel;
            // Create "Font Size" value box.
            TextBox fontSize = new TextBox();
            fontSize.Tag = "Font Size: ";
            fontSize.Style = valueBox;
            // Create the "Font Size" validation binding.
            binding = new Binding();
            binding.Path = new PropertyPath("CurrentFont.Size");
            binding.ValidationRules.Add(new StringToIntValidationRule());
            // Set the binding and add the "Font Size" combo box to it.
            fontSize.SetBinding(TextBox.TextProperty, binding);
            fontPanel.Children.Add(fontSize);
            // Create "Font Colour" value box.
            TextBox fontColour = new TextBox();
            fontColour.Tag = "Font Colour: ";
            fontColour.Style = valueBox;
            // Create the "Font Colour" validation binding.
            binding = new Binding();
            binding.Path = new PropertyPath("CurrentFont.Foreground");
            binding.ValidationRules.Add(new StringToHexValidationRule());
            // Set the binding and add the "Font Colour" combo box to it.
            fontColour.SetBinding(TextBox.TextProperty, binding);
            fontPanel.Children.Add(fontColour);
            // Create the "Font Background" value box.
            TextBox fontBackground = new TextBox();
            fontBackground.Tag = "Font BG: ";
            fontBackground.Style = valueBox;
            // Create the "Font Background" validation binding.
            binding = new Binding();
            binding.Path = new PropertyPath("CurrentFont.Background");
            binding.ValidationRules.Add(new StringToHexValidationRule());
            // Set the binding and add the "Font Background" combo box to it.
            fontBackground.SetBinding(TextBox.TextProperty, binding);
            fontPanel.Children.Add(fontBackground);
            // Add the "Font Panel" stackpanel to MenuContent.
            MenuContent.Add(fontPanel);
        }
    }
}
