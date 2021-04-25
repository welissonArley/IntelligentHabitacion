using FluentAssertions;
using IntelligentHabitacion.Api.Application.UseCases.Home.RegisterHome;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using Xunit;

namespace Validators.Test.Home.RegisterHome
{
    public class RegisterHomeValidationTest
    {
        [Fact]
        public void Validade_Brazil_Sucess()
        {
            var validator = new RegisterHomeValidation();
            var validationResult = validator.Validate(new RequestRegisterHomeJson
            {
                ZipCode = "31.000-000",
                AdditionalAddressInfo = "A1",
                Address = "Rua 1",
                City = "Belo Horizonte",
                Country = IntelligentHabitacion.Communication.Enums.Country.BRAZIL,
                Neighborhood = "Liberdade",
                Number = "771",
                StateProvince = "Minas Gerais"
            });

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validade_OtherCountry_Sucess()
        {
            var validator = new RegisterHomeValidation();
            var validationResult = validator.Validate(new RequestRegisterHomeJson
            {
                ZipCode = "94203",
                AdditionalAddressInfo = "A1",
                Address = "Avenue 1",
                City = "New York",
                Country = IntelligentHabitacion.Communication.Enums.Country.UNITED_STATES,
                Number = "771"
            });

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validade_ZipCodeEmpty()
        {
            var validator = new RegisterHomeValidation();
            var validationResult = validator.Validate(new RequestRegisterHomeJson
            {
                AdditionalAddressInfo = "A1",
                Address = "Avenue 1",
                City = "New York",
                Country = IntelligentHabitacion.Communication.Enums.Country.UNITED_STATES,
                Number = "771"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.ZIPCODE_EMPTY));
        }

        [Fact]
        public void Validade_AdressEmpty()
        {
            var validator = new RegisterHomeValidation();
            var validationResult = validator.Validate(new RequestRegisterHomeJson
            {
                ZipCode = "94203",
                AdditionalAddressInfo = "A1",
                City = "New York",
                Country = IntelligentHabitacion.Communication.Enums.Country.UNITED_STATES,
                Number = "771"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.ADDRESS_EMPTY));
        }

        [Fact]
        public void Validade_NumberEmpty()
        {
            var validator = new RegisterHomeValidation();
            var validationResult = validator.Validate(new RequestRegisterHomeJson
            {
                ZipCode = "94203",
                AdditionalAddressInfo = "A1",
                City = "New York",
                Country = IntelligentHabitacion.Communication.Enums.Country.UNITED_STATES,
                Address = "Avenue 1"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.NUMBER_EMPTY));
        }

        [Fact]
        public void Validade_CityEmpty()
        {
            var validator = new RegisterHomeValidation();
            var validationResult = validator.Validate(new RequestRegisterHomeJson
            {
                ZipCode = "94203",
                AdditionalAddressInfo = "A1",
                Number = "771",
                Country = IntelligentHabitacion.Communication.Enums.Country.UNITED_STATES,
                Address = "Avenue 1"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.CITY_EMPTY));
        }

        [Fact]
        public void Validade_CountryInvalid()
        {
            var validator = new RegisterHomeValidation();
            var validationResult = validator.Validate(new RequestRegisterHomeJson
            {
                ZipCode = "94203",
                AdditionalAddressInfo = "A1",
                Number = "771",
                Country = (IntelligentHabitacion.Communication.Enums.Country)1000,
                Address = "Avenue 1",
                City = "New York"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.COUNTRY_EMPTY));
        }

        [Fact]
        public void Validade_Brazil_StateProvinceEmpty()
        {
            var validator = new RegisterHomeValidation();
            var validationResult = validator.Validate(new RequestRegisterHomeJson
            {
                ZipCode = "31.000-000",
                AdditionalAddressInfo = "A1",
                Address = "Rua 1",
                City = "Belo Horizonte",
                Country = IntelligentHabitacion.Communication.Enums.Country.BRAZIL,
                Neighborhood = "Liberdade",
                Number = "771"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.STATEPROVINCE_EMPTY));
        }

        [Fact]
        public void Validade_Brazil_NeighBorhoodEmpty()
        {
            var validator = new RegisterHomeValidation();
            var validationResult = validator.Validate(new RequestRegisterHomeJson
            {
                ZipCode = "31.000-000",
                AdditionalAddressInfo = "A1",
                Address = "Rua 1",
                City = "Belo Horizonte",
                Country = IntelligentHabitacion.Communication.Enums.Country.BRAZIL,
                StateProvince = "Minas Gerais",
                Number = "771"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.NEIGHBORHOOD_EMPTY));
        }

        [Fact]
        public void Validade_Brazil_ZipCodeInvalid()
        {
            var validator = new RegisterHomeValidation();
            var validationResult = validator.Validate(new RequestRegisterHomeJson
            {
                ZipCode = "3000000",
                AdditionalAddressInfo = "A1",
                Address = "Rua 1",
                City = "Belo Horizonte",
                Neighborhood = "Liberdade",
                Country = IntelligentHabitacion.Communication.Enums.Country.BRAZIL,
                StateProvince = "Minas Gerais",
                Number = "771"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.ZIPCODE_INVALID_BRAZIL));
        }
    }
}
