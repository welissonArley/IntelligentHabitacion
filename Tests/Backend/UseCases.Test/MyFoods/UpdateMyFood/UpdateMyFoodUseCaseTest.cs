using AutoMapper;
using FluentAssertions;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Api.Application.UseCases.MyFoods.UpdateMyFood;
using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System;
using System.Threading.Tasks;
using Useful.ToTests.Builders.CreateResponseUseCase;
using Useful.ToTests.Builders.LoggedUser;
using Useful.ToTests.Builders.Mapper;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Entity;
using Xunit;

namespace UseCases.Test.MyFoods.UpdateMyFood
{
    public class UpdateMyFoodUseCaseTest
    {
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly IntelligentHabitacion.Api.Domain.Entity.User _user;

        public UpdateMyFoodUseCaseTest()
        {
            _user = UserBuilder.Instance().User_WithoutHomeAssociation();

            _unitOfWork = UnitOfWorkBuilder.Instance().Build();
            _intelligentHabitacionUseCase = IntelligentHabitacionUseCaseBuilder.Instance().Build();
            _loggedUser = LoggedUserBuilder.Instance().User(_user).Build();
            _mapper = MapperBuilder.Build();
        }

        [Fact]
        public async Task Validade_Sucess()
        {
            var myFood = CreateFood();
            var repository = MyFoodsUpdateOnlyRepositoryBuilder.Instance().GetById(_user.Id, myFood).Build();

            var useCase = new UpdateMyFoodUseCase(repository, _unitOfWork, _intelligentHabitacionUseCase, _loggedUser, _mapper);

            var validationResult = await useCase.Execute(myFood.Id, new RequestProductJson
            {
                DueDate = DateTime.UtcNow.AddDays(100),
                Manufacturer = "Manufacturer New",
                Name = "Product Edit",
                Quantity = 3,
                Type = IntelligentHabitacion.Communication.Response.Type.Unity
            });

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
            validationResult.ResponseJson.Should().BeNull();

            myFood.Name.Should().Equals("Product Edit");
            myFood.Quantity.Should().Equals(3);
            myFood.Type.Should().Equals(IntelligentHabitacion.Communication.Response.Type.Unity);
            myFood.Manufacturer.Should().Equals("Manufacturer New");
        }

        [Fact]
        public async Task Validate_MyFoodNotFound()
        {
            var repository = MyFoodsUpdateOnlyRepositoryBuilder.Instance().Build();
            var useCase = new UpdateMyFoodUseCase(repository, _unitOfWork, _intelligentHabitacionUseCase, _loggedUser, _mapper);

            Func<Task> act = async () => {
                await useCase.Execute(0, new RequestProductJson
                {
                    DueDate = DateTime.UtcNow.AddDays(100),
                    Manufacturer = "Manufacturer New",
                    Name = "Product Edit",
                    Quantity = 3,
                    Type = IntelligentHabitacion.Communication.Response.Type.Unity
                });
            };

            await act.Should().ThrowAsync<ProductNotFoundException>().WithMessage(ResourceTextException.PRODUCT_NOT_FOUND);
        }

        [Fact]
        public async Task Validade_EmptyName_DueDateYesterday()
        {
            var myFood = CreateFood();
            var repository = MyFoodsUpdateOnlyRepositoryBuilder.Instance().GetById(_user.Id, myFood).Build();

            var useCase = new UpdateMyFoodUseCase(repository, _unitOfWork, _intelligentHabitacionUseCase, _loggedUser, _mapper);
            
            Func<Task> act = async () => {
                await useCase.Execute(myFood.Id, new RequestProductJson
                {
                    DueDate = DateTime.UtcNow.AddDays(-1),
                    Manufacturer = "Manufacturer New",
                    Quantity = 3,
                    Type = IntelligentHabitacion.Communication.Response.Type.Unity
                });
            };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(c => c.ErrorMensages.Count == 2 &&
                c.ErrorMensages.Contains(ResourceTextException.PRODUCT_NAME_EMPTY) &&
                c.ErrorMensages.Contains(ResourceTextException.DUEDATE_MUST_BE_GRATER_THAN_TODAY));
        }

        private MyFood CreateFood()
        {
            return new MyFood
            {
                Id = 1,
                Active = true,
                CreateDate = DateTime.UtcNow,
                Manufacturer = "Manufacturer",
                Name = "Product",
                Quantity = 1,
                Type = IntelligentHabitacion.Api.Domain.Enums.Type.Box,
                UserId = 1
            };
        }
    }
}
