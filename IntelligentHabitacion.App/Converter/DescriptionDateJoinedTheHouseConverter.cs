using IntelligentHabitacion.App.Useful;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.Converter
{
    public class DescriptionDateJoinedTheHouseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            return string.Format(ResourceText.DESCRIPTION_DATE_JOINED_THE_HOUSE, date.ToString(ResourceText.FORMAT_DATE), DateTimeController.DateToStringYearMonthAndDay(date));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
