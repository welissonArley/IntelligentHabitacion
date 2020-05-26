using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.SetOfRules.Rule;
using IntelligentHabitacion.Communication.Response;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntelligentHabitacion.Api.Test.FactoryFake
{
    public class MyFoodFactoryFake : BaseFactoryFake
    {
        public MyFoodRule GetRule()
        {
            return new MyFoodRule(GetLoggedUserAdministrator(), GetRepository());
        }

        public MyFoodRule GetRuleUserWithoutFood()
        {
            return new MyFoodRule(GetLoggedUserAdministrator(), GetRepositoryUserWithoutFood());
        }

        public IMyFoodRepository GetRepository()
        {
            var repositorioMock = new Mock<IMyFoodRepository>();
            repositorioMock.Setup(c => c.Create(new MyFood()));
            repositorioMock.Setup(c => c.GetMyFoods(It.IsAny<long>())).Returns(new List<MyFood>
            {
                new MyFood
                {
                    Quantity = 7,
                    DueDate = DateTime.Now,
                    Id = 1,
                    Manufacturer = "M",
                    Name = "N",
                    UserId = 1,
                    Type = Repository.Model.Type.Kilogram
                }
            }.AsQueryable());
            repositorioMock.Setup(c => c.GetMyFood(1, It.IsAny<long>())).Returns(new MyFood());
            return repositorioMock.Object;
        }
        public IMyFoodRepository GetRepositoryUserWithoutFood()
        {
            var repositorioMock = new Mock<IMyFoodRepository>();
            repositorioMock.Setup(c => c.GetMyFoods(It.IsAny<long>())).Returns(new List<MyFood>().AsQueryable());
            return repositorioMock.Object;
        }
    }
}
