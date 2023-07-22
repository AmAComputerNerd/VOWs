using System;
using System.Globalization;
using System.Windows.Data;

namespace VOWs.Converters
{
    /// <summary>
    /// The <c>StringToIntConverter</c> class acts as an <c>IValueConverter</c> for XAML, quickly and easily converting a <c>string</c> to an <c>int</c>.
    /// </summary>
    class StringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int val;
            return int.TryParse(value.ToString(), out val) ? val : 11;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
