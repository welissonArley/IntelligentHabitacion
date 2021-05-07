using AutoMapper;
using FluentAssertions;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Api.Application.UseCases.User.UserInformations;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Communication.Response;
using System.Threading.Tasks;
using Useful.ToTests.Builders.CreateResponseUseCase;
using Useful.ToTests.Builders.LoggedUser;
using Useful.ToTests.Builders.Mapper;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Entity;
using Xunit;

namespace UseCases.Test.User.UserInformations
{
    public class UserInformationsUseCaseTest
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly IntelligentHabitacion.Api.Domain.Entity.User _user;

        public UserInformationsUseCaseTest()
        {
            _user = UserBuilder.Instance().User_WithoutHomeAssociation();

            _unitOfWork = UnitOfWorkBuilder.Instance().Build();
            _mapper = MapperBuilder.Build();
            _intelligentHabitacionUseCase = IntelligentHabitacionUseCaseBuilder.Instance().Build();
            _loggedUser = LoggedUserBuilder.Instance().User(_user).Build();
        }

        [Fact]
        public async Task Validade_Sucess()
        {
            var useCase = new UserInformationsUseCase(_loggedUser, _mapper, _unitOfWork, _intelligentHabitacionUseCase);

            var validationResult = await useCase.Execute();

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
            validationResult.ResponseJson.Should().BeOfType<ResponseUserInformationsJson>();

            _user.Name.Should().Equals(_user.Name);
            _user.Phonenumbers.Should().HaveCount(_user.Phonenumbers.Count);
            _user.EmergencyContacts.Should().HaveCount(_user.EmergencyContacts.Count);
        }
    }
}
