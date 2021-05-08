using AutoMapper;
using FluentAssertions;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Api.Application.UseCases.Home.UpdateHomeInformations;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Useful.ToTests.Builders.CreateResponseUseCase;
using Useful.ToTests.Builders.LoggedUser;
using Useful.ToTests.Builders.Mapper;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Entity;
using Xunit;

namespace UseCases.Test.Home.UpdateHomeInformations
{
    public class UpdateHomeInformationsUseCaseTest
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateHomeInformationsUseCaseTest()
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
            var home = CreateHome();
            var repository = HomeUpdateOnlyRepositoryBuilder.Instance().GetById(home).Build();
            var useCase = new UpdateHomeInformationsUseCase(_loggedUser, _mapper, _unitOfWork, _intelligentHabitacionUseCase, repository);

            var validationResult = await useCase.Execute(new RequestUpdateHomeJson
            {
                City = "City",
                AdditionalAddressInfo = "Additional",
                Address = "Address",
                Neighborhood = "Neighborhood",
                Number = "1",
                StateProvince = "State",
                ZipCode = "31.000-000",
                NetworksName = "myWifiName",
                NetworksPassword = "password",
                Rooms = new List<string> { "Room" }
            });

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
            validationResult.ResponseJson.Should().BeNull();

            home.NetworksName.Should().Equals("myWifiName");
            home.NetworksPassword.Should().Equals("password");
            home.Rooms.Should().HaveCount(1);
        }

        [Fact]
        public async Task Validade_EmptyZipCode_EmptyCity()
        {
            var home = CreateHome();
            var repository = HomeUpdateOnlyRepositoryBuilder.Instance().GetById(home).Build();
            var useCase = new UpdateHomeInformationsUseCase(_loggedUser, _mapper, _unitOfWork, _intelligentHabitacionUseCase, repository);

            Func<Task> act = async () => {
                await useCase.Execute(new RequestUpdateHomeJson
                {
                    AdditionalAddressInfo = "Additional",
                    Address = "Address",
                    Neighborhood = "Neighborhood",
                    Number = "1",
                    StateProvince = "State",
                    NetworksName = "myWifiName",
                    NetworksPassword = "password",
                    Rooms = new List<string> { "Room" }
                });
            };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMensages.Count == 2 &&
                e.ErrorMensages.Contains(ResourceTextException.CITY_EMPTY) &&
                e.ErrorMensages.Contains(ResourceTextException.ZIPCODE_EMPTY));
        }

        private IntelligentHabitacion.Api.Domain.Entity.Home CreateHome()
        {
            return new IntelligentHabitacion.Api.Domain.Entity.Home
            {
                Id = 1,
                Active = true,
                City = "City",
                AdditionalAddressInfo = "Additional",
                Address = "Address",
                Neighborhood = "Neighborhood",
                Number = "1",
                StateProvince = "State",
                ZipCode = "31.000-000",
                Country = IntelligentHabitacion.Api.Domain.ValueObjects.CountryEnum.BRAZIL,
                DeadlinePaymentRent = 15,
                CreateDate = DateTime.UtcNow
            };
        }
    }
}
