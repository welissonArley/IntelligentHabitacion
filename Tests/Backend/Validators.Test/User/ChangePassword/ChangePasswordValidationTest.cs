using FluentAssertions;
using IntelligentHabitacion.Api.Application.UseCases.User.ChangePassword;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using Useful.ToTests.Builders.Encripter;
using Xunit;

namespace Validators.Test.User.ChangePassword
{
    public class ChangePasswordValidationTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var currentPassword = "@CurrentPassword123";

            var encripter = PasswordEncripterBuilder.Instance().Build();
            var userDataNow = new IntelligentHabitacion.Api.Domain.Entity.User { Password = encripter.Encrypt(currentPassword) };

            var validator = new ChangePasswordValidation(encripter, userDataNow);
            var validationResult = validator.Validate(new RequestChangePasswordJson
            {
                CurrentPassword = currentPassword,
                NewPassword = "@NewPassword123"
            });

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validade_CurrentPasswordInvalid()
        {
            var currentPassword = "@CurrentPassword123";

            var encripter = PasswordEncripterBuilder.Instance().Build();
            var userDataNow = new IntelligentHabitacion.Api.Domain.Entity.User { Password = encripter.Encrypt(currentPassword) };

            var validator = new ChangePasswordValidation(encripter, userDataNow);
            var validationResult = validator.Validate(new RequestChangePasswordJson
            {
                CurrentPassword = "#MyPassword123",
                NewPassword = "@NewPassword123"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.CURRENT_PASSWORD_INVALID));
        }

        [Fact]
        public void Validade_NewPasswordEmpty()
        {
            var currentPassword = "@CurrentPassword123";

            var encripter = PasswordEncripterBuilder.Instance().Build();
            var userDataNow = new IntelligentHabitacion.Api.Domain.Entity.User { Password = encripter.Encrypt(currentPassword) };

            var validator = new ChangePasswordValidation(encripter, userDataNow);
            var validationResult = validator.Validate(new RequestChangePasswordJson
            {
                CurrentPassword = currentPassword,
                NewPassword = ""
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PASSWORD_EMPTY));
        }

        [Fact]
        public void Validade_NewPasswordInvalid()
        {
            var currentPassword = "@CurrentPassword123";

            var encripter = PasswordEncripterBuilder.Instance().Build();
            var userDataNow = new IntelligentHabitacion.Api.Domain.Entity.User { Password = encripter.Encrypt(currentPassword) };

            var validator = new ChangePasswordValidation(encripter, userDataNow);
            var validationResult = validator.Validate(new RequestChangePasswordJson
            {
                CurrentPassword = currentPassword,
                NewPassword = "@"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.INVALID_PASSWORD));
        }
    }
}
