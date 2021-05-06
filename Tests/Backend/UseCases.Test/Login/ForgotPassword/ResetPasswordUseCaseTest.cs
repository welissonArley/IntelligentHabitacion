using FluentAssertions;
using IntelligentHabitacion.Api.Application.Services.Cryptography;
using IntelligentHabitacion.Api.Application.UseCases.Login.ForgotPassword;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Encripter;
using Useful.ToTests.Builders.Repositories;
using Xunit;

namespace UseCases.Test.Login.ForgotPassword
{
    public class ResetPasswordUseCaseTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PasswordEncripter _passwordEncripter;

        public ResetPasswordUseCaseTest()
        {
            _unitOfWork = UnitOfWorkBuilder.Instance().Build();
            _passwordEncripter = PasswordEncripterBuilder.Instance().Build();
        }

        [Fact]
        public async Task Validade_Sucess()
        {
            var email = "user@email.com";
            var newPassword = "@Password123";
            var code = "ABC123";

            var userModel = CreateUserModel(email);
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().GetByEmail(email, userModel).Build();
            var userUpdateOnlyRepository = UserUpdateOnlyRepositoryBuilder.Instance().GetByEmail(email, userModel).Build();
            var codeReadOnlyRepository = CodeReadOnlyRepositoryBuilder.Instance().GetByUserId(userModel.Id, CreateCodeModel(userModel.Id, code)).Build();
            var codeWriteOnlyRepository = CodeWriteOnlyRepositoryBuilder.Instance().Build();

            var useCase = new ResetPasswordUseCase(_passwordEncripter, userUpdateOnlyRepository,
                codeReadOnlyRepository, userReadOnlyRepository, codeWriteOnlyRepository, _unitOfWork);

            Func<Task> act = async () =>
            {
                await useCase.Execute(new RequestResetYourPasswordJson
                {
                    Email = email,
                    Password = newPassword,
                    Code = code
                });
            };

            await act.Should().NotThrowAsync();
            userModel.Password.Should().Equals(_passwordEncripter.Encrypt(newPassword));
        }

        [Fact]
        public async Task Validate_UserNotFound()
        {
            var email = "user@email.com";
            var newPassword = "@Password123";
            var code = "ABC123";

            var userModel = CreateUserModel(email);
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().GetByEmail(email, userModel).Build();
            var userUpdateOnlyRepository = UserUpdateOnlyRepositoryBuilder.Instance().GetByEmail(email, userModel).Build();
            var codeReadOnlyRepository = CodeReadOnlyRepositoryBuilder.Instance().GetByUserId(userModel.Id, CreateCodeModel(userModel.Id, code)).Build();
            var codeWriteOnlyRepository = CodeWriteOnlyRepositoryBuilder.Instance().Build();

            var useCase = new ResetPasswordUseCase(_passwordEncripter, userUpdateOnlyRepository,
                codeReadOnlyRepository, userReadOnlyRepository, codeWriteOnlyRepository, _unitOfWork);

            Func<Task> act = async () =>
            {
                await useCase.Execute(new RequestResetYourPasswordJson
                {
                    Email = "error@email.com",
                    Password = newPassword,
                    Code = code
                });
            };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMensages.Count == 1 && e.ErrorMensages.Contains(ResourceTextException.INVALID_USER));
        }

        private IntelligentHabitacion.Api.Domain.Entity.User CreateUserModel(string email)
        {
            return new IntelligentHabitacion.Api.Domain.Entity.User
            {
                Id = 1,
                Active = true,
                CreateDate = DateTime.UtcNow,
                Email = email,
                Password = "OldPassword"
            };
        }
        private IntelligentHabitacion.Api.Domain.Entity.Code CreateCodeModel(long userId, string code)
        {
            return new IntelligentHabitacion.Api.Domain.Entity.Code
            {
                Active = true,
                Id = 1,
                CreateDate = DateTime.UtcNow,
                Type = IntelligentHabitacion.Api.Domain.Enums.CodeType.ResetPassword,
                UserId = userId,
                Value = code
            };
        }
    }
}
