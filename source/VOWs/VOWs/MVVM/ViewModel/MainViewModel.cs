using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
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

            // Activate ViewModel to receive messages.
            IsActive = true;

            // Assign values for DocumentEdit view.
            DocumentEditVM = new DocumentEditViewModel();
            DocumentEditViewCommand = new RelayCommand(() =>
            {
                CurrentView = DocumentEditVM;
            });
            // Assign values for Settings view.
            SettingsVM = new SettingsViewModel();
            SettingsViewCommand = new RelayCommand(() =>
            {
                CurrentView = SettingsVM;
            });

            // Set CurrentView to default menu (DocumentEditVM).
            CurrentView = DocumentEditVM;
        }

        /// <summary>
        /// The overriden <c>OnActivated</c> method registers the class with a variety of messagers from the Messenger object.
        /// </summary>
        protected override void OnActivated()
        {
            // Register the class to receive ChangeViewEvent messages.
            Messenger.Register<MainViewModel, ChangeViewMessage>(this, (r, m) => r.Receive(m));
            // Register the class to receive RequestStorageMessage messages.
            Messenger.Register<MainViewModel, RequestStorageMessage>(this, (r, m) => r.Reply(m));
            // Register the class to receive UpdateStorageMessage messages.
            Messenger.Register<MainViewModel, UpdateStorageMessage>(this, (r, m) => r.Reply(m));
        }

        /// <summary>
        /// The <c>Receive</c> method will be called whenever the <c>ChangeViewMessage</c> is sent.
        /// </summary>
        /// <param name="message">The event that was sent, with data.</param>
        private void Receive(ChangeViewMessage message)
        {
            if(message.IsExplicit)
            {
                CurrentView = GetViewModel(message.Id);
                return;
            }
            // Fetch both viewmodels.
            object vm1 = GetViewModel(message.Id);
            object vm2 = GetViewModel((int)message.OtherId);
            // Attempt to toggle them.
            if (CurrentView == vm1) CurrentView = vm2;
            else if (CurrentView == vm2) CurrentView = vm1;
        }

        /// <summary>
        /// The <c>Reply</c> method will be called whenever the <c>RequestStorageMessage</c> is sent.
        /// </summary>
        /// <param name="message">The event that was sent to reply to.</param>
        private void Reply(RequestStorageMessage message)
        {
            message.Reply(Storage);
        }

        /// <summary>
        /// The <c>Reply</c> method will be called whenever the <c>UpdateStorageMessage</c> is sent.
        /// </summary>
        /// <param name="message">The event that was sent to reply to, with data.</param>
        private void Reply(UpdateStorageMessage message)
        {
            try
            {
                Storage = message.UpdatedValue;
                message.Reply(true);
            } catch (Exception)
            {
                message.Reply(false);
            }
        }

        /// <summary>
        /// The <c>GetViewModel</c> method converts an Id to it's corresponding ViewModel.
        /// </summary>
        /// <param name="id">The Id of the ViewModel to fetch.</param>
        /// <returns>The ViewModel</returns>
        private object GetViewModel(int id)
        {
            switch(id)
            {
                case 0:
                    return DocumentEditVM;
                case 1:
                    return SettingsVM;
                default:
                    return null;
            }
        }
    }
}
