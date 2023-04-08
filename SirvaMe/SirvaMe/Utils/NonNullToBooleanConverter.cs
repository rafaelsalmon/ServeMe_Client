using System;
using System.Globalization;
using Xamarin.Forms;

namespace SirvaMe.Utils
{
    class NonNullToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is string)
                {
                    return !String.IsNullOrEmpty((string)value);
                }
                if (value is bool)
                {
                    return value;
                }
                if (value is int)
                {
                    return (int)value >= (int)parameter;
                }
                return value != null;
            }
            catch (Exception e)
            {
                return value != null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            return null;
        }
    }
}
