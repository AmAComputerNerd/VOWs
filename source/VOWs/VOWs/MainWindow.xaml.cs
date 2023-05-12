using System.Windows;
using System.Windows.Media;
using VOWs.Core;

namespace VOWs
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public QuickStorage QuickStorage { get; private set; }

        public MainWindow()
        {
            QuickStorage = new QuickStorage();
            InitializeComponent();
            // Assign the DataContext of the main window to QuickStorage for easy XAML access.
            DataContext = QuickStorage;
        }
    }
}
