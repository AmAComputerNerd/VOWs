using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOWsLauncher.MVVM.Model;

namespace VOWsLauncher.MVVM.ViewModel
{
    public class SettingsViewModel
    {
        public Globals Globals { get => Globals.Default; }
    }
}
