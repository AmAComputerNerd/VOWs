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
        private object _currentView;
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
        public DocumentViewModel DocumentVM;

        public RelayCommand DocumentViewCommand;

        public QuickStorage StorageRef;

        public MainViewModel() 
        {
            DocumentVM = new DocumentViewModel();
            CurrentView = DocumentVM;

            DocumentViewCommand = new RelayCommand(o =>
            {
                CurrentView = DocumentVM;
            });

            StorageRef = new QuickStorage();
        }
    }
}
