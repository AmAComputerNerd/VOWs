using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace VOWs.Converters
{
    class StringToImageSourceConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Console.WriteLine(value.ToString());
            string? path = value as string;
            //if (path == null) throw new ArgumentNullException(path);
            if (path == null) return null;
            Uri uri = new Uri(path);
            BitmapImage image = new BitmapImage(uri);
            return (ImageSource)image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
