using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.Converter
{
    public class MonthYearStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;

            var month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);

            return $"{month.First().ToString().ToUpper() + month.Substring(1)}, {date.Year}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
