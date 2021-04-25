using FluentAssertions;
using IntelligentHabitacion.Api.Application.SharedValidators;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using System;
using Xunit;

namespace Validators.Test.MyFoods
{
    public class MyFoodValidationTest
    {
        [Fact]
        public void Validade_Sucess_WithDueDate()
        {
            var validator = new MyFoodValidation();
            var validationResult = validator.Validate(new RequestProductJson
            {
                Name = "Product 1",
                Quantity = 1,
                Type = IntelligentHabitacion.Communication.Response.Type.Box,
                Manufacturer = "Company One",
                DueDate = DateTime.UtcNow.AddDays(50)
            });

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validade_Sucess_WithoutDueDate()
        {
            var validator = new MyFoodValidation();
            var validationResult = validator.Validate(new RequestProductJson
            {
                Name = "Product 1",
                Quantity = 1,
                Type = IntelligentHabitacion.Communication.Response.Type.Box,
                Manufacturer = "Company One"
            });

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validade_NameEmpty()
        {
            var validator = new MyFoodValidation();
            var validationResult = validator.Validate(new RequestProductJson
            {
                Quantity = 1,
                Type = IntelligentHabitacion.Communication.Response.Type.Box,
                Manufacturer = "Company One"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PRODUCT_NAME_EMPTY));
        }

        [Fact]
        public void Validade_QuantityZero()
        {
            var validator = new MyFoodValidation();
            var validationResult = validator.Validate(new RequestProductJson
            {
                Name = "Product",
                Quantity = 0,
                Type = IntelligentHabitacion.Communication.Response.Type.Box,
                Manufacturer = "Company One"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.QUANTITY_PRODUCTS_INVALID));
        }

        [Fact]
        public void Validade_TypeInvalid()
        {
            var validator = new MyFoodValidation();
            var validationResult = validator.Validate(new RequestProductJson
            {
                Name = "Product",
                Quantity = 1,
                Type = (IntelligentHabitacion.Communication.Response.Type)100,
                Manufacturer = "Company One"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.TYPE_PRODUCTS_INVALID));
        }

        [Fact]
        public void Validade_DueDateInvalid()
        {
            var validator = new MyFoodValidation();
            var validationResult = validator.Validate(new RequestProductJson
            {
                Name = "Product",
                Quantity = 1,
                Type = IntelligentHabitacion.Communication.Response.Type.Box,
                Manufacturer = "Company One",
                DueDate = DateTime.UtcNow.AddDays(-1)
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.DUEDATE_MUST_BE_GRATER_THAN_TODAY));
        }
    }
}
