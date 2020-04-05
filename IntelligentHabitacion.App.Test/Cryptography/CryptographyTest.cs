using Xunit;

namespace IntelligentHabitacion.App.Test.Cryptography
{
    public class CryptographyTest
    {
        [Fact]
        public void TestCryptography()
        {
            var context = new SQLite.Cryptography();
            var result = context.Encrypt("Value");

            Assert.True(!result.Equals("Value"));

            result = context.Dencrypt(result);

            Assert.Equal("Value", result);
        }
    }
}
