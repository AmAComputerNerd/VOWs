﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VOWs.Custom
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:VOWs.Custom"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:VOWs.Custom;assembly=VOWs.Custom"
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
    public class Page : Control
    {
        public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(nameof(TextBox), typeof(PageTextBox), typeof(Page));

        public PageTextBox TextBox
        {
            get => (PageTextBox)GetValue(TextBoxProperty);
            set => SetValue(TextBoxProperty, value);
        }

        static Page()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Page), new FrameworkPropertyMetadata(typeof(Page)));
        }
    }
}
