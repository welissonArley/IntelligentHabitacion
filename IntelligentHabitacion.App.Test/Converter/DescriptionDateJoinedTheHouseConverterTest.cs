using IntelligentHabitacion.App.Converter;
using System;
using Xamarin.Forms;
using Xunit;

namespace IntelligentHabitacion.App.Test.Converter
{
    public class DescriptionDateJoinedTheHouseConverterTest
    {
        private readonly IValueConverter _converter;
        public DescriptionDateJoinedTheHouseConverterTest()
        {
            _converter = new DescriptionDateJoinedTheHouseConverter();
        }

        [Fact]
        public void DateAgo()
        {
            var result = _converter.Convert(DateTime.Now.AddDays(-700), null, null, null);
            Assert.True(result.ToString().Length > 0);
        }

        [Fact]
        public void Today()
        {
            var result = _converter.Convert(DateTime.Now, null, null, null);
            Assert.True(result.ToString().Length > 0);
        }

        [Fact]
        public void ConvertBack()
        {
            var result = _converter.ConvertBack(null, null, null, null);
            Assert.True(!(bool)result);
        }
    }
}
