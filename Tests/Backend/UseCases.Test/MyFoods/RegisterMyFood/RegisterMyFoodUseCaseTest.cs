using AutoMapper;
using FluentAssertions;
using HashidsNet;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Api.Application.UseCases.MyFoods.RegisterMyFood;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.MyFoods;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System;
using System.Threading.Tasks;
using Useful.ToTests.Builders.CreateResponseUseCase;
using Useful.ToTests.Builders.Hashids;
using Useful.ToTests.Builders.LoggedUser;
using Useful.ToTests.Builders.Mapper;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Entity;
using Xunit;

namespace UseCases.Test.MyFoods.RegisterMyFood
{
    public class RegisterMyFoodUseCaseTest
    {
        private readonly IHashids _hashIds;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly IMyFoodsWriteOnlyRepository _myFoodsWriteOnlyRepository;

        public RegisterMyFoodUseCaseTest()
        {
            var user = UserBuilder.Instance().User_WithoutHomeAssociation();

            _unitOfWork = UnitOfWorkBuilder.Instance().Build();
            _intelligentHabitacionUseCase = IntelligentHabitacionUseCaseBuilder.Instance().Build();
            _loggedUser = LoggedUserBuilder.Instance().User(user).Build();
            _myFoodsWriteOnlyRepository = MyFoodsWriteOnlyRepositoryBuilder.Instance().Build();
            _mapper = MapperBuilder.Build();
            _hashIds = HashidsBuilder.Instance().Build();
        }

        [Fact]
        public async Task Validade_Sucess()
        {
            var useCase = new RegisterMyFoodUseCase(_myFoodsWriteOnlyRepository, _unitOfWork, _loggedUser, _mapper, _intelligentHabitacionUseCase, _hashIds);

            var validationResult = await useCase.Execute(new RequestProductJson
            {
                DueDate = DateTime.UtcNow.AddDays(100),
                Manufacturer = "Manufacturer",
                Name = "Product",
                Quantity = 1,
                Type = IntelligentHabitacion.Communication.Response.Type.Box
            });

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
            validationResult.ResponseJson.Should().BeOfType<string>();

            var result = validationResult.ResponseJson.As<string>();
            result.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Validade_EmptyName_DueDateYesterday()
        {
            var useCase = new RegisterMyFoodUseCase(_myFoodsWriteOnlyRepository, _unitOfWork, _loggedUser, _mapper, _intelligentHabitacionUseCase, _hashIds);

            Func<Task> act = async () => {
                await useCase.Execute(new RequestProductJson
                {
                    DueDate = DateTime.UtcNow.AddDays(-1),
                    Manufacturer = "Manufacturer",
                    Quantity = 1,
                    Type = IntelligentHabitacion.Communication.Response.Type.Box
                });
            };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(c => c.ErrorMensages.Count == 2 &&
                c.ErrorMensages.Contains(ResourceTextException.PRODUCT_NAME_EMPTY) &&
                c.ErrorMensages.Contains(ResourceTextException.DUEDATE_MUST_BE_GRATER_THAN_TODAY));
        }
    }
}
