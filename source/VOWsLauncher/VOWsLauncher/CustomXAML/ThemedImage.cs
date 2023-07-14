using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VOWsLauncher.MVVM.Model;

namespace VOWsLauncher.CustomXAML
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:VOWsLauncher.CustomXAML"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:VOWsLauncher.CustomXAML;assembly=VOWsLauncher.CustomXAML"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class ThemedImage : Control
    {
        public Globals Globals { get => Globals.Default; }

        public ImageSource CurrentSource
        {
            get => (ImageSource)GetValue(CurrentSourceProperty);
        }

        public ImageSource BackupContent
        {
            get => (ImageSource)GetValue(BackupContentProperty);
            set => SetValue(BackupContentProperty, value);
        }
        public ImageSource LightModeContent 
        {
            get => (ImageSource)GetValue(LightModeContentProperty);
            set => SetValue(LightModeContentProperty, value);
        }
        public ImageSource DarkModeContent
        {
            get => (ImageSource)GetValue(DarkModeContentProperty);
            set => SetValue(DarkModeContentProperty, value);
        }

        public static readonly DependencyProperty CurrentSourceProperty = DependencyProperty.Register(nameof(CurrentSource), typeof(ImageSource), typeof(ThemedImage), new PropertyMetadata(null));

        public static readonly DependencyProperty BackupContentProperty = DependencyProperty.Register(nameof(BackupContent), typeof(ImageSource), typeof(ThemedImage), new PropertyMetadata(null));
        public static readonly DependencyProperty LightModeContentProperty = DependencyProperty.Register(nameof(LightModeContent), typeof(ImageSource), typeof(ThemedImage), new PropertyMetadata(null));
        public static readonly DependencyProperty DarkModeContentProperty = DependencyProperty.Register(nameof(DarkModeContent), typeof(ImageSource), typeof(ThemedImage), new PropertyMetadata(null));

        static ThemedImage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemedImage), new FrameworkPropertyMetadata(typeof(ThemedImage)));
        }
    }
}
