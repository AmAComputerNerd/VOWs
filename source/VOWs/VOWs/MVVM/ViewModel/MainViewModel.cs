using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows;
using VOWs.Events;
using VOWs.MVVM.Model;

namespace VOWs.MVVM.ViewModel
{
    /// <summary>
    /// The <c>MainViewModel</c> class holds data relating to the MainWindow MVVM model as well as global Storage objects.
    /// </summary>
    public partial class MainViewModel : ObservableRecipient
    {
        /// <summary>
        /// The <c>Globals</c> property refers to an object full of shared variables across multiple classes.
        /// These may be read-only, or be able to be edited across classes. The <c>Globals</c> class is an <c>ObservableObject</c>.
        /// </summary>
        public Globals Globals { get => Globals.Default; }

        /// <summary>
        /// The <c>DocumentEditVM</c> property refers to the current instance of the <c>DocumentEditViewModel</c> and accompanying view.
        /// </summary>
        public DocumentEditViewModel DocumentEditVM;

        /// <summary>
        /// The <c>DocumentEditViewCommand</c> command will trigger a change in the <c>CurrentView</c> property, setting it to <c>DocumentEditVM</c>.
        /// </summary>
        public RelayCommand DocumentEditViewCommand;

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
            // Activate ViewModel to receive messages.
            IsActive = true;

            // Assign values for DocumentEdit view.
            DocumentEditVM = new();
            DocumentEditViewCommand = new(() =>
            {
                CurrentView = DocumentEditVM;
            });

            // Set CurrentView to default menu (DocumentEditVM).
            CurrentView = DocumentEditVM;
        }

        /// <summary>
        /// The overriden <c>OnActivated</c> method registers the class with a variety of messagers from the Messenger object.
        /// </summary>
        protected override void OnActivated()
        {
            // Register the class to receive ChangeViewMessage messages.
            Messenger.Register<MainViewModel, ChangeViewMessage>(this, (r, m) => r.Receive(m));
            // Register the class to reply to RequestScaleFactorMessage messages.
            Messenger.Register<MainViewModel, RequestScaleFactorMessage>(this, (r, m) => r.Reply(m));
        }

        /// <summary>
        /// The <c>Receive</c> method will be called whenever the <c>ChangeViewMessage</c> is sent.
        /// </summary>
        /// <param name="message">The event that was sent, with data.</param>
        private void Receive(ChangeViewMessage message)
        {
            // Check if the ChangeViewMessage is explicit (in that it specifies an exact ViewModel to switch to).
            // If so, we'll just set the CurrentView to the ViewModel matching the numeric ID.
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
        /// The <c>Reply</c> method will be called whenever the <c>RequestScaleFactorMessage</c> is sent.
        /// </summary>
        /// <param name="message">The event that was sent.</param>
        private void Reply(RequestScaleFactorMessage message)
        {
            // Retrieve the current size of the application.
            double width = Application.Current.MainWindow.Width;
            double height = Application.Current.MainWindow.Height;
            // Retrieve the current zoom level, where 1.0 is the default (100%).
            double zoomLevel = 1.0;
            // Calculate the scale factors.
            double width_scaleFactor = width * zoomLevel;
            double height_scaleFactor = height * zoomLevel;
            // Reply to the message.
            message.Reply(new double[] {width_scaleFactor, height_scaleFactor});
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
                0 => DocumentEditVM,
                _ => null,
            };
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
