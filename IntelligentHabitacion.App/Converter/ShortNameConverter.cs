using IntelligentHabitacion.Useful;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.Converter
{
    public class ShortNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Name.ShortNameConverter(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
