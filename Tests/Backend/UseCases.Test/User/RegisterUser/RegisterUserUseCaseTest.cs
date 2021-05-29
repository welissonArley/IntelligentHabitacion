using AutoMapper;
using FluentAssertions;
using IntelligentHabitacion.Api.Application.Services.Cryptography;
using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Api.Application.UseCases.User.RegisterUser;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System;
using System.Threading.Tasks;
using Useful.ToTests.Builders.CreateResponseUseCase;
using Useful.ToTests.Builders.Encripter;
using Useful.ToTests.Builders.Mapper;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Requests;
using Xunit;

namespace UseCases.Test.User.RegisterUser
{
    public class RegisterUserUseCaseTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PasswordEncripter _passwordEncripter;
        private readonly IMapper _mapper;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;

        public RegisterUserUseCaseTest()
        {
            _unitOfWork = UnitOfWorkBuilder.Instance().Build();
            _passwordEncripter = PasswordEncripterBuilder.Instance().Build();
            _mapper = MapperBuilder.Build();
            _intelligentHabitacionUseCase = IntelligentHabitacionUseCaseBuilder.Instance().Build();
            _userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();
            _userWriteOnlyRepository = UserWriteOnlyRepositoryBuilder.Instance().Build();
        }

        [Fact]
        public async Task Validade_Sucess()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();

            var useCase = new RegisterUserUseCase(_mapper, _unitOfWork, _intelligentHabitacionUseCase, _userWriteOnlyRepository, _userReadOnlyRepository, _passwordEncripter);

            var validationResult = await useCase.Execute(user);

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
            validationResult.ResponseJson.Should().BeOfType<ResponseUserRegisteredJson>();

            var responseJson = validationResult.ResponseJson.As<ResponseUserRegisteredJson>();
            responseJson.ProfileColor.Should().NotBeNullOrEmpty().And.StartWith("#");
            responseJson.Id.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Validade_Empty_PhoneNumbersAndEmergencyContacts()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();
            user.Phonenumbers.Clear();
            user.EmergencyContacts.Clear();

            var useCase = new RegisterUserUseCase(_mapper, _unitOfWork, _intelligentHabitacionUseCase, _userWriteOnlyRepository, _userReadOnlyRepository, _passwordEncripter);

            Func<Task> act = async () => { await useCase.Execute(user); };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMensages.Count == 2 && 
                    e.ErrorMensages.Contains(ResourceTextException.PHONENUMBER_EMPTY)
                    && e.ErrorMensages.Contains(ResourceTextException.EMERGENCYCONTACT_EMPTY));
        }
    }
}
