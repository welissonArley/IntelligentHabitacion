using AutoMapper;
using FluentAssertions;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Api.Application.UseCases.MyFoods.GetMyFoods;
using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Communication.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Useful.ToTests.Builders.CreateResponseUseCase;
using Useful.ToTests.Builders.LoggedUser;
using Useful.ToTests.Builders.Mapper;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Entity;
using Xunit;

namespace UseCases.Test.MyFoods.GetMyFoods
{
    public class GetMyFoodsUseCaseTest
    {
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly IntelligentHabitacion.Api.Domain.Entity.User _user;

        public GetMyFoodsUseCaseTest()
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
            var foods = CreateFoods();
            var myFoodsReadOnlyRepository = MyFoodsReadOnlyRepositoryBuilder.Instance().GetByUserId(_user.Id, foods).Build();
            var useCase = new GetMyFoodsUseCase(myFoodsReadOnlyRepository, _mapper, _loggedUser, _unitOfWork, _intelligentHabitacionUseCase);

            var validationResult = await useCase.Execute();

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
            validationResult.ResponseJson.Should().BeOfType<List<ResponseMyFoodJson>>();

            var result = validationResult.ResponseJson.As<List<ResponseMyFoodJson>>();
            result.Should().HaveCount(foods.Count);
        }

        private IList<MyFood> CreateFoods()
        {
            return new List<MyFood>
            {
                new MyFood
                {
                    Id = 1,
                    Active = true,
                    CreateDate = DateTime.UtcNow,
                    Manufacturer = "Manufacturer",
                    Name = "Product",
                    Quantity = 1,
                    Type = IntelligentHabitacion.Api.Domain.Enums.Type.Box,
                    UserId = 1
                }
            };
        }
    }
}
