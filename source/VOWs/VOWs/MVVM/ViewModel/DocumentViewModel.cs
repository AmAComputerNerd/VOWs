using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VOWs.Core;

namespace VOWs.MVVM.ViewModel
{
    class DocumentViewModel
    {
        public MainViewModel Context;
        public Image? ImageContext { get; set; }
        public VideoDrawing? VideoContext { get; set; }

        public DocumentViewModel(MainViewModel Context)
        {
            this.Context = Context;
            ImageContext = null;
            VideoContext = null;
        }
    }
}
