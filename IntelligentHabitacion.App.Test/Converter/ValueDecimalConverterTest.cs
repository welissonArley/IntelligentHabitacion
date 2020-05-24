using IntelligentHabitacion.App.Converter;
using System;
using Xamarin.Forms;
using Xunit;

namespace IntelligentHabitacion.App.Test.Converter
{
    public class ValueDecimalConverterTest
    {
        private readonly IValueConverter _converter;
        public ValueDecimalConverterTest()
        {
            _converter = new ValueDecimalConverter();
        }

        [Fact]
        public void NullValue()
        {
            var result = _converter.Convert(null, null, null, null);
            Assert.Null(result);
        }

        [Fact]
        public void DecimalValue()
        {
            var result = _converter.Convert((decimal)7, null, null, null);
            Assert.True(result is decimal);
        }

        [Fact]
        public void ConvertBackWithNullParameter()
        {
            var result = _converter.ConvertBack(null, null, null, null);
            Assert.Equal(0, (decimal)result);
        }

        [Fact]
        public void ConvertBackWithObjectParameter()
        {
            var result = _converter.ConvertBack("ABC", null, null, null);
            Assert.Equal(0, (decimal)result);
        }

        [Fact]
        public void ConvertBackWithValue()
        {
            var result = _converter.ConvertBack("7", null, null, null);
            Assert.Equal(7, (decimal)result);
        }
    }
}
