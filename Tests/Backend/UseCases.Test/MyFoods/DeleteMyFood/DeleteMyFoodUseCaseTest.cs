using FluentAssertions;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Api.Application.UseCases.MyFoods.DeleteMyFood;
using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.MyFoods;
using IntelligentHabitacion.Exception;
using System;
using System.Threading.Tasks;
using Useful.ToTests.Builders.CreateResponseUseCase;
using Useful.ToTests.Builders.LoggedUser;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Entity;
using Xunit;

namespace UseCases.Test.MyFoods.DeleteMyFood
{
    public class DeleteMyFoodUseCaseTest
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly IntelligentHabitacion.Api.Domain.Entity.User _user;
        private readonly IMyFoodsWriteOnlyRepository _myFoodsWriteOnlyRepository;

        public DeleteMyFoodUseCaseTest()
        {
            _user = UserBuilder.Instance().User_WithoutHomeAssociation();

            _unitOfWork = UnitOfWorkBuilder.Instance().Build();
            _intelligentHabitacionUseCase = IntelligentHabitacionUseCaseBuilder.Instance().Build();
            _loggedUser = LoggedUserBuilder.Instance().User(_user).Build();
            _myFoodsWriteOnlyRepository = MyFoodsWriteOnlyRepositoryBuilder.Instance().Build();
        }

        [Fact]
        public async Task Validade_Sucess()
        {
            var food = CreateFood();
            var myFoodsReadOnlyRepository = MyFoodsReadOnlyRepositoryBuilder.Instance().GetById(_user.Id, food).Build();
            var useCase = new DeleteMyFoodUseCase(myFoodsReadOnlyRepository, _myFoodsWriteOnlyRepository, _unitOfWork, _intelligentHabitacionUseCase, _loggedUser);

            var validationResult = await useCase.Execute(food.Id);

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
            validationResult.ResponseJson.Should().BeNull();
        }

        [Fact]
        public async Task Validate_MyFoodNotFound()
        {
            var myFoodsReadOnlyRepository = MyFoodsReadOnlyRepositoryBuilder.Instance().Build();
            var useCase = new DeleteMyFoodUseCase(myFoodsReadOnlyRepository, _myFoodsWriteOnlyRepository, _unitOfWork, _intelligentHabitacionUseCase, _loggedUser);

            Func<Task> act = async () => {
                await useCase.Execute(0);
            };

            await act.Should().ThrowAsync<ProductNotFoundException>().WithMessage(ResourceTextException.PRODUCT_NOT_FOUND);
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
