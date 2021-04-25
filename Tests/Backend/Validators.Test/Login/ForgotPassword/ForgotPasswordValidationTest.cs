using FluentAssertions;
using IntelligentHabitacion.Api.Application.UseCases.Login.ForgotPassword;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using System;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Repositories;
using Xunit;

namespace Validators.Test.Login.ForgotPassword
{
    public class ForgotPasswordValidationTest
    {
        [Fact]
        public async Task Validade_Sucess()
        {
            var codeValue = "ABC123";
            var email = "user@test.com";
            long userId = 1;

            var codeRepository = CodeReadOnlyRepositoryBuilder.Instance().GetByUserId(userId, CreateCodeModel(userId, codeValue));

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().GetByEmail(email, CreateUserModel(userId, email));

            var validator = new ForgotPasswordValidation(codeRepository.Build(), userReadOnlyRepository.Build());
            var validationResult = await validator.ValidateAsync(new RequestResetYourPasswordJson
            {
                Email = email,
                Code = codeValue,
                Password = "@Password123"
            });

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Validade_UserInvalid()
        {
            var codeValue = "ABC123";
            var email = "user@test.com";
            long userId = 1;

            var codeRepository = CodeReadOnlyRepositoryBuilder.Instance().GetByUserId(userId, CreateCodeModel(userId, codeValue));

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().GetByEmail(email, CreateUserModel(userId, email));

            var validator = new ForgotPasswordValidation(codeRepository.Build(), userReadOnlyRepository.Build());
            var validationResult = await validator.ValidateAsync(new RequestResetYourPasswordJson
            {
                Email = "invaliduser@test.com",
                Code = codeValue,
                Password = "@Password123"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.INVALID_USER));
        }

        [Fact]
        public async Task Validade_CodeInvalid()
        {
            var codeValue = "ABC123";
            var email = "user@test.com";
            long userId = 1;

            var codeRepository = CodeReadOnlyRepositoryBuilder.Instance().GetByUserId(2, CreateCodeModel(2, codeValue));

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().GetByEmail(email, CreateUserModel(userId, email));

            var validator = new ForgotPasswordValidation(codeRepository.Build(), userReadOnlyRepository.Build());
            var validationResult = await validator.ValidateAsync(new RequestResetYourPasswordJson
            {
                Email = email,
                Code = codeValue,
                Password = "@Password123"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.CODE_RESET_PASSWORD_REQUIRED));
        }

        [Fact]
        public async Task Validade_PasswordEmpty()
        {
            var codeValue = "ABC123";
            var email = "user@test.com";
            long userId = 1;

            var codeRepository = CodeReadOnlyRepositoryBuilder.Instance().GetByUserId(userId, CreateCodeModel(userId, codeValue));

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().GetByEmail(email, CreateUserModel(userId, email));

            var validator = new ForgotPasswordValidation(codeRepository.Build(), userReadOnlyRepository.Build());
            var validationResult = await validator.ValidateAsync(new RequestResetYourPasswordJson
            {
                Email = email,
                Code = codeValue,
                Password = ""
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PASSWORD_EMPTY));
        }

        [Fact]
        public async Task Validade_PasswordInvalid()
        {
            var codeValue = "ABC123";
            var email = "user@test.com";
            long userId = 1;

            var codeRepository = CodeReadOnlyRepositoryBuilder.Instance().GetByUserId(userId, CreateCodeModel(userId, codeValue));

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().GetByEmail(email, CreateUserModel(userId, email));

            var validator = new ForgotPasswordValidation(codeRepository.Build(), userReadOnlyRepository.Build());
            var validationResult = await validator.ValidateAsync(new RequestResetYourPasswordJson
            {
                Email = email,
                Code = codeValue,
                Password = "@"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.INVALID_PASSWORD));
        }

        [Fact]
        public async Task Validade_CodeNotTheSame()
        {
            var codeValue = "ABC123";
            var email = "user@test.com";
            long userId = 1;

            var codeRepository = CodeReadOnlyRepositoryBuilder.Instance().GetByUserId(userId, CreateCodeModel(userId, codeValue));

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().GetByEmail(email, CreateUserModel(userId, email));

            var validator = new ForgotPasswordValidation(codeRepository.Build(), userReadOnlyRepository.Build());
            var validationResult = await validator.ValidateAsync(new RequestResetYourPasswordJson
            {
                Email = email,
                Code = "DEF456",
                Password = "@Password123"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.CODE_INVALID));
        }

        [Fact]
        public async Task Validade_ExpiredCode()
        {
            var codeValue = "ABC123";
            var email = "user@test.com";
            long userId = 1;

            var codeRepository = CodeReadOnlyRepositoryBuilder.Instance().GetByUserId(userId, CreateCodeModel(userId, codeValue, DateTime.UtcNow.AddDays(-1)));

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().GetByEmail(email, CreateUserModel(userId, email));

            var validator = new ForgotPasswordValidation(codeRepository.Build(), userReadOnlyRepository.Build());
            var validationResult = await validator.ValidateAsync(new RequestResetYourPasswordJson
            {
                Email = email,
                Code = codeValue,
                Password = "@Password123"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EXPIRED_CODE));
        }

        private IntelligentHabitacion.Api.Domain.Entity.Code CreateCodeModel(long userId, string codeValue, DateTime? createDate = null)
        {
            return new IntelligentHabitacion.Api.Domain.Entity.Code
            {
                Active = true,
                CreateDate = createDate ?? DateTime.UtcNow,
                Type = IntelligentHabitacion.Api.Domain.Enums.CodeType.ResetPassword,
                UserId = userId,
                Value = codeValue
            };
        }
        private IntelligentHabitacion.Api.Domain.Entity.User CreateUserModel(long userId, string email)
        {
            return new IntelligentHabitacion.Api.Domain.Entity.User
            {
                Id = userId,
                Active = true,
                CreateDate = DateTime.UtcNow,
                Email = email
            };
        }
    }
}
