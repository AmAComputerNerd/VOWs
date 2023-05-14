using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOWs.Core;

namespace VOWs.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public QuickStorage QuickStorage { get; private set; }

        public MainViewModel() 
        {
            this.QuickStorage = new QuickStorage();
        }
    }
}
