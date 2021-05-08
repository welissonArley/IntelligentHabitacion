using FluentAssertions;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Api.Application.UseCases.MyFoods.ChangeQuantityOfOneProduct;
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

namespace UseCases.Test.MyFoods.ChangeQuantityOfOneProduct
{
    public class ChangeQuantityOfOneProductUseCaseTest
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly IntelligentHabitacion.Api.Domain.Entity.User _user;
        private readonly IMyFoodsWriteOnlyRepository _myFoodsWriteOnlyRepository;

        public ChangeQuantityOfOneProductUseCaseTest()
        {
            _user = UserBuilder.Instance().User_WithoutHomeAssociation();

            _unitOfWork = UnitOfWorkBuilder.Instance().Build();
            _intelligentHabitacionUseCase = IntelligentHabitacionUseCaseBuilder.Instance().Build();
            _loggedUser = LoggedUserBuilder.Instance().User(_user).Build();
            _myFoodsWriteOnlyRepository = MyFoodsWriteOnlyRepositoryBuilder.Instance().Build();
        }

        [Theory]
        [InlineData(2)]
        [InlineData(-3)]
        public async Task Validade_Sucess(int amount)
        {
            var food = CreateFood();
            var myFoodsReadOnlyRepository = MyFoodsReadOnlyRepositoryBuilder.Instance().GetById(_user.Id, food).Build();
            var useCase = new ChangeQuantityOfOneProductUseCase(myFoodsReadOnlyRepository, _myFoodsWriteOnlyRepository, _unitOfWork, _intelligentHabitacionUseCase, _loggedUser);

            var validationResult = await useCase.Execute(food.Id, amount);

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
            validationResult.ResponseJson.Should().BeNull();
        }

        [Fact]
        public async Task Validate_MyFoodNotFound()
        {
            var myFoodsReadOnlyRepository = MyFoodsReadOnlyRepositoryBuilder.Instance().Build();
            var useCase = new ChangeQuantityOfOneProductUseCase(myFoodsReadOnlyRepository, _myFoodsWriteOnlyRepository, _unitOfWork, _intelligentHabitacionUseCase, _loggedUser);

            Func<Task> act = async () => {
                await useCase.Execute(0, 1);
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
