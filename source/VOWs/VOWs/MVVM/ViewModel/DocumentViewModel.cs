using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VOWs.MVVM.Model;

namespace VOWs.MVVM.ViewModel
{
    class DocumentViewModel
    {
        public QuickStorage QuickStorage { get; private set; }
        public Image? ImageContext { get; set; }
        public VideoDrawing? VideoContext { get; set; }

        public DocumentViewModel()
        {
            // Retrieve the storage reference from the MainViewModel. This isn't the best practise, but tbh, I can't think of another way to do it except by static access.
            QuickStorage = ((MainViewModel)Application.Current.MainWindow.DataContext).StorageRef;
            // These can be set to null until a user clicks on an image or video - this will store the position and other relevant information on these sections.
            ImageContext = null;
            VideoContext = null;
        }
    }
}
