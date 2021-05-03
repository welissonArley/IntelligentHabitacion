using AutoMapper;
using FluentAssertions;
using IntelligentHabitacion.Api.Application.Services.Cryptography;
using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Api.Application.UseCases.Login.DoLogin;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System;
using System.Threading.Tasks;
using Useful.ToTests.Builders.CreateResponseUseCase;
using Useful.ToTests.Builders.Encripter;
using Useful.ToTests.Builders.Mapper;
using Useful.ToTests.Builders.Repositories;
using Xunit;

namespace UseCases.Test.Login.DoLogin
{
    public class LoginUseCaseTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PasswordEncripter _passwordEncripter;
        private readonly IMapper _mapper;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;

        public LoginUseCaseTest()
        {
            _unitOfWork = UnitOfWorkBuilder.Instance().Build();
            _passwordEncripter = PasswordEncripterBuilder.Instance().Build();
            _mapper = MapperBuilder.Build();
            _intelligentHabitacionUseCase = IntelligentHabitacionUseCaseBuilder.Instance().Build();
        }

        [Fact]
        public async Task Validade_Sucess()
        {
            var email = "user@email.com";
            var password = "@Password123";

            var userRepository = UserReadOnlyRepositoryBuilder.Instance().GetByEmailPassword(email, _passwordEncripter.Encrypt(password)).Build();

            var useCase = new LoginUseCase(userRepository, _passwordEncripter, _intelligentHabitacionUseCase, _mapper, _unitOfWork);

            var validationResult = await useCase.Execute(new RequestLoginJson
            {
                User = email,
                Password = password
            });

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.ResponseJson.Should().BeOfType<ResponseLoginJson>();

            var responseLogin = validationResult.ResponseJson.As<ResponseLoginJson>();
            responseLogin.Name.Should().NotBeNullOrEmpty();
            responseLogin.ProfileColor.Should().NotBeNullOrEmpty();
            responseLogin.Id.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Validade_UserNotFound()
        {
            var email = "user@email.com";
            var password = "@Password123";

            var userRepository = UserReadOnlyRepositoryBuilder.Instance().GetByEmailPassword(email, "#WrongPassword").Build();

            var useCase = new LoginUseCase(userRepository, _passwordEncripter, _intelligentHabitacionUseCase, _mapper, _unitOfWork);

            Func<Task> act = async () => { await useCase.Execute(new RequestLoginJson
            {
                User = email,
                Password = password
            }); };

            await act.Should().ThrowAsync<InvalidLoginException>().WithMessage(ResourceTextException.USER_OR_PASSWORD_INVALID);
        }
    }
}
