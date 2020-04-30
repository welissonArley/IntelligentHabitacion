using IntelligentHabitacion.App.Useful;
using System.ComponentModel;
using Xunit;

namespace IntelligentHabitacion.App.Test.Useful
{
    public class GetEnumDescriptionTest
    {
        public enum EnumTest
        {
            WithoutDescription = 0,
            [Description("TITLE_UNITY")]
            WithDescription = 1
        }

        [Fact]
        public void WithouDescription()
        {
            var result = GetEnumDescription.Description(EnumTest.WithoutDescription);
            Assert.Equal("WithoutDescription", result);
        }

        [Fact]
        public void WithDescription()
        {
            var result = GetEnumDescription.Description(EnumTest.WithDescription);
            Assert.NotEqual("WithDescription", result);
        }
    }
}
