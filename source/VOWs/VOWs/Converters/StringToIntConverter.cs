using System;
using System.Globalization;
using System.Windows.Data;

namespace VOWs.Converters
{
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
