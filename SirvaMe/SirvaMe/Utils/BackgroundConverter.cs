using System;
using System.Globalization;
using Xamarin.Forms;

namespace SirvaMe.Utils
{
    public class BackgroundConverter : IValueConverter
    {
        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var index = System.Convert.ToInt16(value);
                return index % 2 == 0 ? Color.FromHex("#ddd") : Color.White;
            }
            catch (Exception ex)
            {
                return Color.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}