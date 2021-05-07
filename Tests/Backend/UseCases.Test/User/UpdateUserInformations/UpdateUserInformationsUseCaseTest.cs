using AutoMapper;
using FluentAssertions;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Api.Application.UseCases.User.EmailAlreadyBeenRegistered;
using IntelligentHabitacion.Api.Application.UseCases.User.UpdateUserInformations;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Useful.ToTests.Builders.CreateResponseUseCase;
using Useful.ToTests.Builders.LoggedUser;
using Useful.ToTests.Builders.Mapper;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Entity;
using Xunit;

namespace UseCases.Test.User.UpdateUserInformations
{
    public class UpdateUserInformationsUseCaseTest
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository;
        private readonly IntelligentHabitacion.Api.Domain.Entity.User _user;

        public UpdateUserInformationsUseCaseTest()
        {
            _user = UserBuilder.Instance().User_WithoutHomeAssociation();

            _unitOfWork = UnitOfWorkBuilder.Instance().Build();
            _mapper = MapperBuilder.Build();
            _intelligentHabitacionUseCase = IntelligentHabitacionUseCaseBuilder.Instance().Build();
            _userUpdateOnlyRepository = UserUpdateOnlyRepositoryBuilder.Instance().GetById(_user).Build();
            _loggedUser = LoggedUserBuilder.Instance().User(_user).Build();
        }

        [Fact]
        public async Task Validade_Sucess()
        {
            var emailAlreadyBeenRegisteredUseCase = new Mock<IEmailAlreadyBeenRegisteredUseCase>();
            emailAlreadyBeenRegisteredUseCase.Setup(c => c.Execute(_user.Email)).ReturnsAsync(new IntelligentHabitacion.Communication.Boolean.BooleanJson { Value = false });

            var useCase = new UpdateUserInformationsUseCase(_loggedUser, _mapper, _userUpdateOnlyRepository, _unitOfWork, emailAlreadyBeenRegisteredUseCase.Object, _intelligentHabitacionUseCase);

            var validationResult = await useCase.Execute(new RequestUpdateUserJson
            {
                Email = _user.Email,
                Name = "User Edit",
                Phonenumbers = new List<string> { "+55 7 7777-7777" },
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact",
                        Phonenumber = "+55 9 5555-5555",
                        Relationship = "Sister"
                    }
                }
            });

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
            validationResult.ResponseJson.Should().BeNull();

            _user.Name.Should().Equals("User Edit");
            _user.Phonenumbers.Should().HaveCount(1);
            _user.EmergencyContacts.Should().HaveCount(1);
            _user.Phonenumbers.First().Number.Should().Equals("+55 7 7777-7777");
            _user.EmergencyContacts.First().Phonenumber.Should().Equals("+55 9 5555-5555");
            _user.EmergencyContacts.First().Relationship.Should().Equals("Sister");
        }

        [Fact]
        public async Task Validade_NewEmailRegistered()
        {
            var emailAlreadyBeenRegisteredUseCase = new Mock<IEmailAlreadyBeenRegisteredUseCase>();
            emailAlreadyBeenRegisteredUseCase.Setup(c => c.Execute("newEmail@email.com")).ReturnsAsync(new IntelligentHabitacion.Communication.Boolean.BooleanJson { Value = true });

            var useCase = new UpdateUserInformationsUseCase(_loggedUser, _mapper, _userUpdateOnlyRepository, _unitOfWork, emailAlreadyBeenRegisteredUseCase.Object, _intelligentHabitacionUseCase);

            Func<Task> act = async () => {
                await useCase.Execute(new RequestUpdateUserJson
                {
                    Email = "newEmail@email.com",
                    Name = "User Edit",
                    Phonenumbers = new List<string> { "+55 7 7777-7777" },
                    EmergencyContacts = new List<RequestEmergencyContactJson>
                    {
                        new RequestEmergencyContactJson
                        {
                            Name = "Contact",
                            Phonenumber = "+55 9 5555-5555",
                            Relationship = "Sister"
                        }
                    }
                });
            };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMensages.Count == 1 &&
                    e.ErrorMensages.Contains(ResourceTextException.EMAIL_ALREADYBEENREGISTERED));
        }
    }
}
