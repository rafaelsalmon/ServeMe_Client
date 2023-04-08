using System;
using Xamarin.Forms;

namespace SirvaMe.CustomControls
{
    class RatingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var rating = (int)value;

            if (rating > 0)
                App.Rating = (rating % 5) == 0 ? 5 : (rating % 5);
            else
                App.Rating = 0;

            //switch (rating)
            //{
            //    case 1:
            //        return "Disappointed!";
            //    case 2:
            //        return "Not a fan!";
            //    case 3:
            //        return "It's Ok!";
            //    case 4:
            //        return "Like it!";
            //    case 5:
            //        return "Love it!";
            //}
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
