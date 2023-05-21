using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VOWs.Core;
using VOWs.MVVM.Model;

namespace VOWs.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public QuickStorage Storage;

        /// <summary>
        /// The private field storing the ViewModel of the Current View.
        /// </summary>
        private object? _currentView;
        /// <summary>
        /// The public field exposing the '_currentView' field.
        /// 
        /// This should be the field accessed to set the view, as the other won't call
        /// OnPropertyChanged().
        /// </summary>
        public object CurrentView
        {
            get
            {
                return _currentView;
            }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// The public field exposing the instance of DocumentViewModel.
        /// </summary>
        public DocumentViewModel DocumentVM;

        /// <summary>
        /// The public field exposing the RelayCommand that will switch the CurrentView
        /// to 'DocumentVM'.
        /// </summary>
        public RelayCommand DocumentViewCommand;

        /// <summary>
        /// Constructor <c>MainViewModel</c> creates an instance of this class, only to be 
        /// called by the Application as new DataContext for the MainWindow.
        /// 
        /// If really needed, it can be accessed AFTER the application has loaded through
        /// <c>((MainViewModel)Application.Current.MainWindow.DataContext)</c>.
        /// </summary>
        public MainViewModel() 
        {
            // Initialise QuickStorage object.
            Storage = new QuickStorage();

            // Initialise ViewModels.
            DocumentVM = new DocumentViewModel();

            // Initialise DocumentViewCommand to switch the CurrentView when executed.
            DocumentViewCommand = new RelayCommand(o =>
            {
                CurrentView = DocumentVM;
            });

            // Set default view for the application - DocumentVM.
            CurrentView = DocumentVM;
        }
    }
}
