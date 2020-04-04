using IntelligentHabitacion.App.SetOfRules.Rule;
using IntelligentHabitacion.App.Test.Factory;
using IntelligentHabitacion.Communication;
using IntelligentHabitacion.Exception;
using Moq;
using Xunit;

namespace IntelligentHabitacion.App.Test.SetOfRulesTest
{
    public class UserRuleTest
    {
        private readonly UserRule _userRule;

        public UserRuleTest()
        {
            _userRule = new UserRule(GetMokIntelligentHabitacionHttpClient(), new SQlite().GetMokSQLite());
        }

        [Fact]
        public async System.Threading.Tasks.Task ValidateEmailEmailAlreadyBeenRegister()
        {
            await Assert.ThrowsAsync<EmailAlreadyBeenRegisteredException>(() => _userRule.ValidateEmailAndVerifyIfAlreadyBeenRegistered("exist@gmail.com"));
        }

        [Fact]
        public void ValidateEmailEmailNotRegister()
        {
            try
            {
                _userRule.ValidateEmail("dontexist@gmail.com");
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void ValidateEmergencyContactInvalidDegreeKinship()
        {
            Assert.Throws<DegreeKinshipEmptyException>(() => _userRule.ValidateEmergencyContact("Name", "(31) 9 9999-9999", ""));
        }

        [Fact]
        public void ValidateEmergencyContactSucess()
        {
            try
            {
                _userRule.ValidateEmergencyContact("Name", "(31) 9 9999-9999", "Mother");
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void ValidateNameEmpty()
        {
            Assert.Throws<NameEmptyException>(() => _userRule.ValidateName(""));
        }

        [Fact]
        public void ValidateNameSucess()
        {
            try
            {
                _userRule.ValidateName("Name");
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void ValidatePasswordEmpty()
        {
            Assert.Throws<PasswordEmptyException>(() => _userRule.ValidatePassword("", ""));
        }

        [Fact]
        public void ValidatePasswordNotSameConfirmation()
        {
            Assert.Throws<PasswordIsNotSameConfirmationException>(() => _userRule.ValidatePassword("123456", "1234567"));
        }

        [Fact]
        public void ValidatePasswordInvalid()
        {
            Assert.Throws<InvalidPasswordException>(() => _userRule.ValidatePassword("123", "123"));
        }

        [Fact]
        public void ValidatePasswordSucess()
        {
            try
            {
                _userRule.ValidatePassword("123456", "123456");
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void ValidatePhoneNumberInvalid()
        {
            Assert.Throws<PhoneNumberInvalidException>(() => _userRule.ValidatePhoneNumber("(319 9999-9999", "(31) 9 9999-9998"));
        }

        [Fact]
        public void ValidatePhoneNumberSucess()
        {
            try
            {
                _userRule.ValidatePhoneNumber("(31) 9 9999-9999", "(31) 9 9999-9998");
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async void CreateSucess()
        {
            try
            {
                await _userRule.Create(new Model.RegisterUserModel
                {
                    Name = "Name",
                    Email = "dontexist@gmail.com",
                    Password = "123456",
                    PasswordConfirmation = "123456",
                    PhoneNumber1 = "(31) 9 9999-9999",
                    PhoneNumber2 = "(31) 9 9999-9998",
                    EmergencyContact1 = new Model.EmergencyContactModel
                    {
                        Name = "Name",
                        FamilyRelationship = "Mother",
                        PhoneNumber = "(31) 9 9999-9999"
                    },
                    EmergencyContact2 = new Model.EmergencyContactModel
                    {
                        Name = "Name",
                        FamilyRelationship = "Mother",
                        PhoneNumber = "(31) 9 9999-9999"
                    }
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
            mock.Setup(c => c.CreateUser(new Communication.Request.RequestRegisterUserJson(), ""));
            mock.Setup(c => c.EmailAlreadyBeenRegistered("exist@gmail.com", null)).ReturnsAsync(new Communication.Boolean.BooleanJson
            {
                Value = true
            });
            mock.Setup(c => c.EmailAlreadyBeenRegistered("dontexist@gmail.com", null)).ReturnsAsync(new Communication.Boolean.BooleanJson
            {
                Value = false
            });

            return mock.Object;
        }
    }
}
