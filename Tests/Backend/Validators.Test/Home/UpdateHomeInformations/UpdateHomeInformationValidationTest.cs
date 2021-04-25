using FluentAssertions;
using IntelligentHabitacion.Api.Application.UseCases.Home.UpdateHomeInformations;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using System.Collections.Generic;
using Xunit;

namespace Validators.Test.Home.UpdateHomeInformations
{
    public class UpdateHomeInformationValidationTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var validator = new UpdateHomeInformationValidation();
            var validationResult = validator.Validate(new RequestUpdateHomeJson
            {
                ZipCode = "94203",
                AdditionalAddressInfo = "A1",
                Address = "Avenue 1",
                City = "New York",
                Number = "771",
                Rooms = new List<string> { "Kitchen" }
            });

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validade_ZipCodeEmpty()
        {
            var validator = new UpdateHomeInformationValidation();
            var validationResult = validator.Validate(new RequestUpdateHomeJson
            {
                AdditionalAddressInfo = "A1",
                Address = "Avenue 1",
                City = "New York",
                Number = "771",
                Rooms = new List<string> { "Kitchen" }
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.ZIPCODE_EMPTY));
        }

        [Fact]
        public void Validade_AdressEmpty()
        {
            var validator = new UpdateHomeInformationValidation();
            var validationResult = validator.Validate(new RequestUpdateHomeJson
            {
                ZipCode = "94203",
                AdditionalAddressInfo = "A1",
                City = "New York",
                Number = "771",
                Rooms = new List<string> { "Kitchen" }
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.ADDRESS_EMPTY));
        }

        [Fact]
        public void Validade_NumberEmpty()
        {
            var validator = new UpdateHomeInformationValidation();
            var validationResult = validator.Validate(new RequestUpdateHomeJson
            {
                ZipCode = "94203",
                AdditionalAddressInfo = "A1",
                City = "New York",
                Address = "Avenue 1",
                Rooms = new List<string> { "Kitchen" }
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.NUMBER_EMPTY));
        }

        [Fact]
        public void Validade_CityEmpty()
        {
            var validator = new UpdateHomeInformationValidation();
            var validationResult = validator.Validate(new RequestUpdateHomeJson
            {
                ZipCode = "94203",
                AdditionalAddressInfo = "A1",
                Number = "771",
                Address = "Avenue 1",
                Rooms = new List<string> { "Kitchen" }
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.CITY_EMPTY));
        }

        [Fact]
        public void Validade_DuplicatedRooms()
        {
            var validator = new UpdateHomeInformationValidation();
            var validationResult = validator.Validate(new RequestUpdateHomeJson
            {
                ZipCode = "94203",
                AdditionalAddressInfo = "A1",
                Number = "771",
                Address = "Avenue 1",
                City = "Belo Horizonte",
                Rooms = new List<string> { "Kitchen", "Kitchen" }
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.THERE_ARE_DUPLICATED_ROOMS));
        }
    }
}
