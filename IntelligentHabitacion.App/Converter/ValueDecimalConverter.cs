using System;
using System.Globalization;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.Converter
{
    public class ValueDecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Format("{0:n}", 0);

            var valueDecimal = (decimal)value;
            return string.Format("{0:n}", valueDecimal);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            if (string.IsNullOrEmpty(strValue))
                strValue = "0";

            if (decimal.TryParse(strValue, out decimal resultdecimal))
                return resultdecimal;

            return (decimal)0.0;
        }
    }
}
