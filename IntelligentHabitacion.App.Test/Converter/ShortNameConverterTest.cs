using IntelligentHabitacion.App.Converter;
using Xamarin.Forms;
using Xunit;

namespace IntelligentHabitacion.App.Test.Converter
{
    public class ShortNameConverterTest
    {
        private readonly IValueConverter _converter;
        public ShortNameConverterTest()
        {
            _converter = new ShortNameConverter();
        }

        [Fact]
        public void NameString()
        {
            var result = _converter.Convert("User test", null, null, null);
            Assert.Equal("UT", result.ToString());
        }

        [Fact]
        public void EmptyString()
        {
            var result = _converter.Convert("", null, null, null);
            Assert.Equal("", result.ToString());
        }

        [Fact]
        public void ConvertBack()
        {
            var result = _converter.ConvertBack(null, null, null, null);
            Assert.True(!(bool)result);
        }
    }
}
