using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace VOWs.Converters
{
    class StringToBitmapImageConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string? path = (string?)value;
            if (path == null) return new BitmapImage(new Uri("/Resources/Images/404-notfound.png", UriKind.RelativeOrAbsolute));
            Uri uri = new(path, UriKind.RelativeOrAbsolute);
            return new BitmapImage(uri);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((BitmapImage)value).UriSource.ToString();
        }
    }
}
