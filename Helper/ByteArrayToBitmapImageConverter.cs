using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace GamingReviews.Helper
{
    [ValueConversion(typeof(byte[]), typeof(BitmapImage))]
    public class ByteArrayToBitmapImageConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte[] rawImageData = value as byte[];
            if (rawImageData == null)
                return null;

            var bitmapImage = new BitmapImage();
            using (var stream = new MemoryStream(rawImageData))
            {
                try
                {
                    bitmapImage.BeginInit();
                    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitmapImage.CacheOption = BitmapCacheOption.Default;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    return new BitmapImage(new Uri(@"/GamingReviews;component/res/Images/no image.png", UriKind.Relative));
                }
            }
            return bitmapImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
