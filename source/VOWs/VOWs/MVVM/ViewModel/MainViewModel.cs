using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Runtime.CompilerServices;
using VOWs.Events;
using VOWs.MVVM.Model;

namespace VOWs.MVVM.ViewModel
{
    /// <summary>
    /// The <c>MainViewModel</c> class holds data relating to the MainWindow MVVM model as well as global Storage objects.
    /// </summary>
    public class MainViewModel : ObservableRecipient
    {
        /// <summary>
        /// The <c>Storage</c> parameter is a reference to the database connected to the application.
        /// It acts as an interface between the application and the database, allowing access and limited edit abilities on variables.
        /// </summary>
        public DatabaseWrapper Storage;

        /// <summary>
        /// The <c>DocumentEditVM</c> parameter refers to the current instance of the <c>DocumentEditViewModel</c> and accompanying view.
        /// </summary>
        public DocumentEditViewModel DocumentEditVM;
        /// <summary>
        /// The <c>DocumentEditViewCommand</c> command will trigger a change in the <c>CurrentView</c> parameter, setting it to <c>DocumentEditVM</c>.
        /// </summary>
        public RelayCommand DocumentEditViewCommand;
        /// <summary>
        /// The <c>SettingsVM</c> parameter refers to the current instance of the <c>SettingsViewModel</c> and accompanying view.
        /// </summary>
        public SettingsViewModel SettingsVM;
        /// <summary>
        /// The <c>SettingsViewCommand</c> command will trigger a change in the <c>CurrentView</c> parameter, setting it to <c>SettingsVM</c>.
        /// </summary>
        public RelayCommand SettingsViewCommand;

        /// <summary>
        /// The <c>_currentView</c> private parameter is the storage object behind <c>CurrentView</c>.
        /// </summary>
        private object _currentView;
        /// <summary>
        /// The <c>CurrentView</c> parameter exposes the currently assigned ViewModel to the program, linking to the View to display.
        /// </summary>
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// A constructor for <c>MainViewModel</c> that assigns default values to all class variables.
        /// </summary>
        public MainViewModel()
        {
            // Assign values for Storage-related variables.
            Storage = new DatabaseWrapper();

            // Assign values for DocumentEdit view.
            DocumentEditVM = new DocumentEditViewModel(this);
            DocumentEditViewCommand = new RelayCommand(() =>
            {
                CurrentView = DocumentEditVM;
            });
            // Assign values for Settings view.
            SettingsVM = new SettingsViewModel(this);
            SettingsViewCommand = new RelayCommand(() =>
            {
                CurrentView = SettingsVM;
            });

            // Register this class to the Messenger service for all view tokens.
            Messenger.Register<MainViewModel, ChangeViewEvent, string>(this, "VOWsButtonToggle", (_, _) =>
            {
                if(CurrentView != null || CurrentView == SettingsVM)
                {
                    DocumentEditViewCommand.Execute(null);
                } 
                else if(CurrentView == DocumentEditVM)
                {
                    SettingsViewCommand.Execute(null);
                }
            });

            // Set CurrentView to default menu (DocumentEditVM).
            CurrentView = DocumentEditVM;
        }
    }
}
