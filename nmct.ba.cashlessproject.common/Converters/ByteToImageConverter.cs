using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace nmct.ba.cashlessproject.common.Converters
{
    public class ByteToImageConverter : IValueConverter
    {
        public static byte[] ImageToBytes(System.Drawing.Image imageIn) {
            if (imageIn == null) return new byte[0];
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static Image BytesToImage(byte[] byteArrayIn) {
            if (byteArrayIn == null || byteArrayIn.Length <= 1) return null;
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                byte[] input = (byte[])value;
                if (input.Length <= 1)
                    return null;
                else
                    return BytesToImage(input);
            }
            else return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value is Image)
            {
                return ImageToBytes(value as Image);
            }
            else return null;
        }
    }
}
