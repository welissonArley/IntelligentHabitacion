using IntelligentHabitacion.App.SetOfRules.Rule;
using IntelligentHabitacion.Communication;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using Moq;
using Xunit;

namespace IntelligentHabitacion.App.Test.SetOfRulesTest
{
    public class LoginRuleTest
    {
        private readonly LoginRule _loginRule;

        public LoginRuleTest()
        {
            _loginRule = new LoginRule(GetMokIntelligentHabitacionHttpClient());
        }

        [Fact]
        public void LoginEmptyPassword()
        {
            Assert.ThrowsAsync<PasswordEmptyException>(async () => await _loginRule.Login("exist@gmail.com", ""));
        }

        [Fact]
        public async void LoginSucess()
        {
            try
            {
                await _loginRule.Login("user@email.com", "123456");
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        private IIntelligentHabitacionHttpClient GetMokIntelligentHabitacionHttpClient()
        {
            var mock = new Mock<IIntelligentHabitacionHttpClient>();
            mock.Setup(c => c.Login(It.IsAny<RequestLoginJson>(), "")).ReturnsAsync(new ResponseJson
            {
                Token = "",
                Response = new ResponseLoginJson
                {
                    Name = "User",
                    IsAdministrator = false,
                    IsPartOfOneHome = false
                }
            });

            return mock.Object;
        }
    }
}
