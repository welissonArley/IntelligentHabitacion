using IntelligentHabitacion.App.Converter;
using Xamarin.Forms;
using Xunit;

namespace IntelligentHabitacion.App.Test.Converter
{
    public class InverseBoolConverterTest
    {
        private readonly IValueConverter _converter;
        public InverseBoolConverterTest()
        {
            _converter = new InverseBoolConverter();
        }

        [Fact]
        public void InverterBool()
        {
            var result = _converter.Convert(true, null, null, null);
            Assert.Equal(false, result);

            result = _converter.Convert(false, null, null, null);
            Assert.Equal(true, result);
        }

        [Fact]
        public void ConvertBack()
        {
            var result = _converter.ConvertBack(true, null, null, null);
            Assert.Equal(true, result);

            result = _converter.ConvertBack(false, null, null, null);
            Assert.Equal(false, result);
        }
    }
}
