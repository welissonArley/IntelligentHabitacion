using FluentAssertions;
using IntelligentHabitacion.Api.Application.Services.Cryptography;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Api.Application.UseCases.User.ChangePassword;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System;
using System.Threading.Tasks;
using Useful.ToTests.Builders.CreateResponseUseCase;
using Useful.ToTests.Builders.Encripter;
using Useful.ToTests.Builders.LoggedUser;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Entity;
using Xunit;

namespace UseCases.Test.User.ChangePassword
{
    public class ChangePasswordUseCaseTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PasswordEncripter _passwordEncripter;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository;

        public ChangePasswordUseCaseTest()
        {
            var user = UserBuilder.Instance().User_WithoutHomeAssociation();

            _unitOfWork = UnitOfWorkBuilder.Instance().Build();
            _passwordEncripter = PasswordEncripterBuilder.Instance().Build();
            _intelligentHabitacionUseCase = IntelligentHabitacionUseCaseBuilder.Instance().Build();
            _loggedUser = LoggedUserBuilder.Instance().User(user).Build();
            _userUpdateOnlyRepository = UserUpdateOnlyRepositoryBuilder.Instance().GetById(user).Build();
        }

        [Fact]
        public async Task Validade_Sucess()
        {
            var useCase = new ChangePasswordUseCase(_loggedUser, _userUpdateOnlyRepository, _unitOfWork, _passwordEncripter, _intelligentHabitacionUseCase);

            var validationResult = await useCase.Execute(new RequestChangePasswordJson
            {
                CurrentPassword = "@Password123",
                NewPassword = "@NewPassword123"
            });

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
            validationResult.ResponseJson.Should().BeNull();
        }

        [Fact]
        public async Task Validade_CurrentPasswordInvalid()
        {
            var useCase = new ChangePasswordUseCase(_loggedUser, _userUpdateOnlyRepository, _unitOfWork, _passwordEncripter, _intelligentHabitacionUseCase);

            Func<Task> act = async () =>
            {
                await useCase.Execute(new RequestChangePasswordJson
                {
                    CurrentPassword = "@InvalidPassword",
                    NewPassword = "@NewPassword123"
                });
            };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMensages.Count == 1 && e.ErrorMensages.Contains(ResourceTextException.CURRENT_PASSWORD_INVALID));
        }
    }
}
