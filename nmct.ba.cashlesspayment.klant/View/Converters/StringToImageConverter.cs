using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace nmct.ba.cashlessproject.klant.Converters
{
    public class StringToImageConverter : IValueConverter
    {
        //private Uri noimage = new Uri(@"pack://application:,,,/nmct.ba.cashlessproject.klant;component/noimage.png");
        private Uri noimage = new Uri(AppDomain.CurrentDomain.BaseDirectory + "noimage.png");

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                byte[] input = (byte[])value;
                if (input.Length <= 1)
                    return new BitmapImage(noimage);
                else
                    return BitmapImageFromBytes(input);
            }
            else
                return new BitmapImage(noimage);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static Image ImageFromBytes(byte[] bytes)
        {
            BitmapImage image = null;
            MemoryStream stream = null;
            try
            {
                stream = new MemoryStream(bytes);
                stream.Seek(0, SeekOrigin.Begin);
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                return img;
            } catch (Exception)
            {
                throw;
            }

        }

        public static BitmapImage BitmapImageFromBytes(byte[] bytes)
        {
            BitmapImage image = null;
            MemoryStream stream = null;
            try
            {
                stream = new MemoryStream(bytes);
                stream.Seek(0, SeekOrigin.Begin);
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                image = new BitmapImage();
                image.BeginInit();
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                image.StreamSource = ms;
                image.StreamSource.Seek(0, SeekOrigin.Begin);
                image.EndInit();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                stream.Close();
                stream.Dispose();
            }
            return image;
        }
    }
}
