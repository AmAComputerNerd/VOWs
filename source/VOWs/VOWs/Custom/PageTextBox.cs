using CommunityToolkit.Mvvm.Messaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using VOWs.Events;

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
    public class PageTextBox : RichTextBox
    {
        // Fields.
        private FontFamily _currentFontFamily;
        private double _currentFontSize;
        private FontStyle _currentFontStyle;
        private FontWeight _currentFontWeight;
        private Brush _currentForeground;

        public static readonly DependencyProperty CurrentFontFamilyProperty = DependencyProperty.Register(nameof(CurrentFontFamily), typeof(FontFamily), typeof(PageTextBox), new(new FontFamily("calibri")));
        public static readonly DependencyProperty CurrentFontSizeProperty = DependencyProperty.Register(nameof(CurrentFontSize), typeof(double), typeof(PageTextBox), new(11d));
        public static readonly DependencyProperty CurrentFontStyleProperty = DependencyProperty.Register(nameof(CurrentFontStyle), typeof(FontStyle), typeof(PageTextBox), new(FontStyles.Normal));
        public static readonly DependencyProperty CurrentFontWeightProperty = DependencyProperty.Register(nameof(CurrentFontWeight), typeof(FontWeight), typeof(PageTextBox), new(FontWeights.Normal));
        public static readonly DependencyProperty CurrentForegroundProperty = DependencyProperty.Register(nameof(CurrentForeground), typeof(Brush), typeof(PageTextBox));

        // Properties.
        public FontFamily CurrentFontFamily
        {
            get => (FontFamily)GetValue(CurrentFontFamilyProperty);
            set
            {
                WeakReferenceMessenger.Default.Send(new LogMessage("About to set CurrentFontFamily.", ToString(), LogLevel.INFO));
                SetValue(CurrentFontFamilyProperty, value);
                if (!Selection.IsEmpty)
                {
                    WeakReferenceMessenger.Default.Send(new LogMessage(Selection.Text, ToString(), LogLevel.INFO));
                    Selection.ApplyPropertyValue(FontSizeProperty, value);
                }
            }
        }
        public double CurrentFontSize
        {
            get => (double)GetValue(CurrentFontSizeProperty);
            set
            {
                SetValue(CurrentFontSizeProperty, value);
                if (!Selection.IsEmpty)
                {
                    Selection.ApplyPropertyValue(FontSizeProperty, value);
                }
            }
        }
        public FontStyle CurrentFontStyle
        {
            get => (FontStyle)GetValue(CurrentFontStyleProperty);
            set
            {
                SetValue(CurrentFontStyleProperty, value);
                if (!Selection.IsEmpty)
                {
                    Selection.ApplyPropertyValue(FontStyleProperty, value);
                }
            }
        }
        public FontWeight CurrentFontWeight
        {
            get => (FontWeight)GetValue(CurrentFontWeightProperty);
            set
            {
                SetValue(CurrentFontWeightProperty, value);
                if (!Selection.IsEmpty)
                {
                    Selection.ApplyPropertyValue(FontWeightProperty, value);
                }
            }
        }
        public Brush CurrentForeground
        {
            get => (Brush)GetValue(CurrentForegroundProperty);
            set
            {
                SetValue(CurrentForegroundProperty, value);
                if (!Selection.IsEmpty)
                {
                    Selection.ApplyPropertyValue(ForegroundProperty, value);
                }
            }
        }

        static PageTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PageTextBox), new FrameworkPropertyMetadata(typeof(PageTextBox)));
        }

        private static void OnCurrentFontChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            if (FontHasChanged())
            {
                TextPointer textPointer = CaretPosition.GetInsertionPosition(LogicalDirection.Forward);
                Run run = new(e.Text, textPointer);
                run.FontFamily = CurrentFontFamily;
                run.FontSize = CurrentFontSize;
                run.FontStyle = CurrentFontStyle;
                run.FontWeight = CurrentFontWeight;
                run.Foreground = CurrentForeground;
                CaretPosition = run.ElementEnd;
                _currentFontFamily = CurrentFontFamily;
                _currentFontSize = CurrentFontSize;
                _currentFontStyle = CurrentFontStyle;
                _currentFontWeight = CurrentFontWeight;
                _currentForeground = CurrentForeground;
            } 
            else
            {
                base.OnTextInput(e);
            }
        }

        private bool FontHasChanged()
        {
            if (_currentFontFamily == null || !CurrentFontFamily.Equals(_currentFontFamily)) return true;
            if (_currentFontSize <= 0 || !CurrentFontSize.Equals(_currentFontSize)) return true;
            if (!CurrentFontStyle.Equals(_currentFontStyle)) return true;
            if (!CurrentFontWeight.Equals(_currentFontWeight)) return true;
            if (!CurrentForeground.Equals(_currentForeground)) return true;
            return false;
        }
    }
}
