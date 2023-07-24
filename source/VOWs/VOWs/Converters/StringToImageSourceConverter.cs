using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace VOWs.Converters
{
    class StringToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Uri.TryCreate(value.ToString(), UriKind.RelativeOrAbsolute, out Uri uri)) {
                return new BitmapImage(uri);
            }
            return new BitmapImage();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((BitmapImage)value).BaseUri.ToString();
        }
    }
}
