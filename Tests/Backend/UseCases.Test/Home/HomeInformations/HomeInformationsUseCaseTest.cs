using AutoMapper;
using FluentAssertions;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Api.Application.UseCases.Home.HomeInformations;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Communication.Response;
using System;
using System.Threading.Tasks;
using Useful.ToTests.Builders.CreateResponseUseCase;
using Useful.ToTests.Builders.LoggedUser;
using Useful.ToTests.Builders.Mapper;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Entity;
using Xunit;

namespace UseCases.Test.Home.HomeInformations
{
    public class HomeInformationsUseCaseTest
    {
        private readonly IMapper _mapper;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;

        public HomeInformationsUseCaseTest()
        {
            var user = UserBuilder.Instance().User_WithHomeAssociation();

            _unitOfWork = UnitOfWorkBuilder.Instance().Build();
            _mapper = MapperBuilder.Build();
            _intelligentHabitacionUseCase = IntelligentHabitacionUseCaseBuilder.Instance().Build();
            _loggedUser = LoggedUserBuilder.Instance().User(user).Build();
        }

        [Fact]
        public async Task Validade_Sucess()
        {
            var useCase = new HomeInformationsUseCase(_loggedUser, _mapper, _unitOfWork, _intelligentHabitacionUseCase);

            var validationResult = await useCase.Execute();

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
            validationResult.ResponseJson.Should().BeOfType<ResponseHomeInformationsJson>();
        }
    }
}
