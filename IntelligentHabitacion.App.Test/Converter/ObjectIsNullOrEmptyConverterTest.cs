using IntelligentHabitacion.App.Converter;
using Xamarin.Forms;
using Xunit;

namespace IntelligentHabitacion.App.Test.Converter
{
    public class ObjectIsNullOrEmptyConverterTest
    {
        private readonly IValueConverter _converter;

        public ObjectIsNullOrEmptyConverterTest()
        {
            _converter = new ObjectIsNullOrEmptyConverter();
        }

        [Fact]
        public void EmptyString()
        {
            var result = _converter.Convert("", null, null, null);
            Assert.True((bool)result);
        }

        [Fact]
        public void ObjectNull()
        {
            var result = _converter.Convert(null, null, null, null);
            Assert.True((bool)result);
        }

        [Fact]
        public void ConvertBack()
        {
            var result = _converter.ConvertBack(null, null, null, null);
            Assert.True(!(bool)result);
        }
    }
}
