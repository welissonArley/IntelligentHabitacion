using AutoMapper;
using FluentAssertions;
using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Api.Application.UseCases.Home.RegisterHome;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.Home;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.API;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System;
using System.Threading.Tasks;
using Useful.ToTests.Builders.CreateResponseUseCase;
using Useful.ToTests.Builders.LoggedUser;
using Useful.ToTests.Builders.Mapper;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Entity;
using Xunit;

namespace UseCases.Test.Home.RegisterHome
{
    public class RegisterHomeUseCaseTest
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly IMapper _mapper;
        private readonly IHomeWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterHomeUseCaseTest()
        {
            _unitOfWork = UnitOfWorkBuilder.Instance().Build();
            _mapper = MapperBuilder.Build();
            _intelligentHabitacionUseCase = IntelligentHabitacionUseCaseBuilder.Instance().Build();
            _repository = HomeWriteOnlyRepositoryBuilder.Instance().Build();
        }

        [Theory]
        [InlineData(IntelligentHabitacion.Communication.Enums.Country.BRAZIL, "31.000-000")]
        [InlineData(IntelligentHabitacion.Communication.Enums.Country.UNITED_STATES, "32805")]
        public async Task Validade_Sucess(IntelligentHabitacion.Communication.Enums.Country country, string zipCode)
        {
            var user = UserBuilder.Instance().User_WithoutHomeAssociation();
            var loggedUser = LoggedUserBuilder.Instance().User(user).Build();
            var useCase = new RegisterHomeUseCase(_repository, _unitOfWork, loggedUser, _mapper, _intelligentHabitacionUseCase);

            var validationResult = await useCase.Execute(new RequestRegisterHomeJson
            {
                City = "City",
                AdditionalAddressInfo = "Additional",
                Address = "Address",
                Country = country,
                Neighborhood = "Neighborhood",
                Number = "1",
                StateProvince = "State",
                ZipCode = zipCode
            });

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
            validationResult.ResponseJson.Should().BeNull();
        }

        [Fact]
        public async Task Validade_EmptyZipCode_InvalidCountry()
        {
            var user = UserBuilder.Instance().User_WithoutHomeAssociation();
            var loggedUser = LoggedUserBuilder.Instance().User(user).Build();
            var useCase = new RegisterHomeUseCase(_repository, _unitOfWork, loggedUser, _mapper, _intelligentHabitacionUseCase);

            Func<Task> act = async () => {
                await useCase.Execute(new RequestRegisterHomeJson
                {
                    City = "City",
                    AdditionalAddressInfo = "Additional",
                    Address = "Address",
                    Country = (IntelligentHabitacion.Communication.Enums.Country)1000,
                    Neighborhood = "Neighborhood",
                    Number = "1",
                    StateProvince = "State"
                });
            };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMensages.Count == 2 &&
                e.ErrorMensages.Contains(ResourceTextException.COUNTRY_EMPTY) &&
                e.ErrorMensages.Contains(ResourceTextException.ZIPCODE_EMPTY));
        }

        [Fact]
        public async Task Validade_UserIsPartOfAHome()
        {
            var user = UserBuilder.Instance().User_WithHomeAssociation();
            var loggedUser = LoggedUserBuilder.Instance().User(user).Build();
            var useCase = new RegisterHomeUseCase(_repository, _unitOfWork, loggedUser, _mapper, _intelligentHabitacionUseCase);

            Func<Task> act = async () => {
                await useCase.Execute(new RequestRegisterHomeJson
                {
                    City = "City",
                    AdditionalAddressInfo = "Additional",
                    Address = "Address",
                    Country = (IntelligentHabitacion.Communication.Enums.Country)1000,
                    Neighborhood = "Neighborhood",
                    Number = "1",
                    StateProvince = "State"
                });
            };

            await act.Should().ThrowAsync<UserIsPartOfAHomeException>().WithMessage(ResourceTextException.USER_IS_PART_OF_A_HOME);
        }
    }
}
