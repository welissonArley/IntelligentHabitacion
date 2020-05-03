using IntelligentHabitacion.App.Converter;
using System;
using Xamarin.Forms;
using Xunit;

namespace IntelligentHabitacion.App.Test.Converter
{
    public class DateToStringConverterTest
    {
        private readonly IValueConverter _converter;
        public DateToStringConverterTest()
        {
            _converter = new DateToStringConverter();
        }

        [Fact]
        public void DateString()
        {
            var result = _converter.Convert(DateTime.Today, null, null, null);
            Assert.True(!string.IsNullOrWhiteSpace(result.ToString()));
        }

        [Fact]
        public void ConvertBack()
        {
            var result = _converter.ConvertBack(null, null, null, null);
            Assert.Null(result);
        }
    }
}
