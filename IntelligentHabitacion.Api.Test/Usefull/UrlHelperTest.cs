using IntelligentHabitacion.Communication.Url;
using Xunit;

namespace IntelligentHabitacion.Api.Test.Usefull
{
    public class UrlHelperTest
    {
        [Fact]
        public void UrlIntelligentHabitacionApi()
        {
            Assert.True(!string.IsNullOrWhiteSpace(UrlHelper.IntelligentHabitacionApi));
        }
    }
}
