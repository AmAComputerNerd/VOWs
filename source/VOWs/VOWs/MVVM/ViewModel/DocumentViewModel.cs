using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VOWs.MVVM.Model;

namespace VOWs.MVVM.ViewModel
{
    class DocumentViewModel
    {
        /// <summary>
        /// The public field exposing the <c>QuickStorage</c> instance.
        /// 
        /// This is mostly for XAML use, as it can be accessed in code elsewhere with
        /// <c>QuickStorage.Instance</c>.
        /// </summary>
        public QuickStorage Storage => QuickStorage.Instance;

        public DocumentViewModel() { }
    }
}
