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

        [Fact]
        public async void RequestCode()
        {
            try
            {
                await _loginRule.RequestCode("user@email.com");
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void ChangePasswordForgotPasswordInvalidCode()
        {
            Assert.ThrowsAsync<CodeEmptyException>(async () => await _loginRule.ChangePasswordForgotPassword(new Model.ForgetPasswordModel
            {
                Email = "exist@gmail.com",
                CodeReceived = "",
                NewPassword = "",
                PasswordConfirmation = ""
            }));
        }

        [Fact]
        public async void ChangePasswordForgotPasswordSucess()
        {
            try
            {
                await _loginRule.ChangePasswordForgotPassword(new Model.ForgetPasswordModel
                {
                    Email = "exist@gmail.com",
                    CodeReceived = "1234",
                    NewPassword = "@Password123",
                    PasswordConfirmation = "@Password123"
                });
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
            mock.Setup(c => c.RequestCodeResetPassword(It.IsAny<string>(), It.IsAny<string>()));

            return mock.Object;
        }
    }
}
