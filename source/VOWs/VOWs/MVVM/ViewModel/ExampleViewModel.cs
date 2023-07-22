using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOWs.MVVM.Model;

namespace VOWs.MVVM.ViewModel
{
    public class ExampleViewModel
    {
        public Globals Globals { get => Globals.Default; }
    }
}
