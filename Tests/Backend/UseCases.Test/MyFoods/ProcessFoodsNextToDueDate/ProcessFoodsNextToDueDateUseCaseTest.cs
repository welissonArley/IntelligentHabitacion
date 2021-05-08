using FluentAssertions;
using IntelligentHabitacion.Api.Application.UseCases.MyFoods.ProcessFoodsNextToDueDate;
using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository.MyFoods;
using IntelligentHabitacion.Api.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Useful.ToTests.Builders.PushNotificationService;
using Useful.ToTests.Builders.Repositories;
using Xunit;

namespace UseCases.Test.MyFoods.ProcessFoodsNextToDueDate
{
    public class ProcessFoodsNextToDueDateUseCaseTest
    {
        private readonly IMyFoodsWriteOnlyRepository _myFoodsWriteOnlyRepository;
        private readonly IPushNotificationService _pushNotificationService;

        public ProcessFoodsNextToDueDateUseCaseTest()
        {
            _myFoodsWriteOnlyRepository = MyFoodsWriteOnlyRepositoryBuilder.Instance().Build();
            _pushNotificationService = PushNotificationServiceBuilder.Instance().Build();
        }

        [Fact]
        public async Task Validade_Sucess()
        {
            var foods = CreateFoods();
            var repository = MyFoodsReadOnlyRepositoryBuilder.Instance().GetExpiredOrCloseToDueDate(foods).Build();

            var useCase = new ProcessFoodsNextToDueDateUseCase(_myFoodsWriteOnlyRepository, repository, _pushNotificationService);

            Func<Task> act = async () =>
            {
                await useCase.Execute();
            };

            await act.Should().NotThrowAsync();
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
                    DueDate = DateTime.UtcNow.AddDays(7).Date,
                    Name = "Product 1",
                    Manufacturer = "Manufacturer",
                    Quantity = 1,
                    Type = IntelligentHabitacion.Api.Domain.Enums.Type.Unity,
                    UserId = 1,
                    User = new IntelligentHabitacion.Api.Domain.Entity.User { Id = 1 }
                },
                new MyFood
                {
                    Id = 2,
                    Active = true,
                    CreateDate = DateTime.UtcNow,
                    DueDate = DateTime.UtcNow.AddDays(3).Date,
                    Name = "Product 2",
                    Manufacturer = "Manufacturer",
                    Quantity = 1,
                    Type = IntelligentHabitacion.Api.Domain.Enums.Type.Unity,
                    UserId = 1,
                    User = new IntelligentHabitacion.Api.Domain.Entity.User { Id = 1 }
                },
                new MyFood
                {
                    Id = 3,
                    Active = true,
                    CreateDate = DateTime.UtcNow,
                    DueDate = DateTime.UtcNow.AddDays(1).Date,
                    Name = "Product 3",
                    Manufacturer = "Manufacturer",
                    Quantity = 1,
                    Type = IntelligentHabitacion.Api.Domain.Enums.Type.Unity,
                    UserId = 1,
                    User = new IntelligentHabitacion.Api.Domain.Entity.User { Id = 1 }
                },
                new MyFood
                {
                    Id = 4,
                    Active = true,
                    CreateDate = DateTime.UtcNow,
                    DueDate = DateTime.UtcNow.Date,
                    Name = "Product 4",
                    Manufacturer = "Manufacturer",
                    Quantity = 1,
                    Type = IntelligentHabitacion.Api.Domain.Enums.Type.Unity,
                    UserId = 1,
                    User = new IntelligentHabitacion.Api.Domain.Entity.User { Id = 1 }
                },
                new MyFood
                {
                    Id = 5,
                    Active = true,
                    CreateDate = DateTime.UtcNow,
                    DueDate = DateTime.UtcNow.AddDays(-1).Date,
                    Name = "Product 5",
                    Manufacturer = "Manufacturer",
                    Quantity = 1,
                    Type = IntelligentHabitacion.Api.Domain.Enums.Type.Unity,
                    UserId = 1,
                    User = new IntelligentHabitacion.Api.Domain.Entity.User { Id = 1 }
                },
                new MyFood
                {
                    Id = 6,
                    Active = true,
                    CreateDate = DateTime.UtcNow,
                    DueDate = DateTime.UtcNow.AddDays(-2).Date,
                    Name = "Product 6",
                    Manufacturer = "Manufacturer",
                    Quantity = 1,
                    Type = IntelligentHabitacion.Api.Domain.Enums.Type.Unity,
                    UserId = 1,
                    User = new IntelligentHabitacion.Api.Domain.Entity.User { Id = 1 }
                },
                new MyFood
                {
                    Id = 7,
                    Active = true,
                    CreateDate = DateTime.UtcNow,
                    DueDate = DateTime.UtcNow.AddDays(-3).Date,
                    Name = "Product 7",
                    Manufacturer = "Manufacturer",
                    Quantity = 1,
                    Type = IntelligentHabitacion.Api.Domain.Enums.Type.Unity,
                    UserId = 1,
                    User = new IntelligentHabitacion.Api.Domain.Entity.User { Id = 1 }
                }
            };
        }
    }
}
