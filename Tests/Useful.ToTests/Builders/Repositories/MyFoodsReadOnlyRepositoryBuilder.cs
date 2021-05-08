using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository.MyFoods;
using Moq;
using System.Collections.Generic;

namespace Useful.ToTests.Builders.Repositories
{
    public class MyFoodsReadOnlyRepositoryBuilder
    {
        private static MyFoodsReadOnlyRepositoryBuilder _instance;
        private readonly Mock<IMyFoodsReadOnlyRepository> _repository;

        private MyFoodsReadOnlyRepositoryBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<IMyFoodsReadOnlyRepository>();
            }
        }

        public static MyFoodsReadOnlyRepositoryBuilder Instance()
        {
            _instance = new MyFoodsReadOnlyRepositoryBuilder();
            return _instance;
        }

        public MyFoodsReadOnlyRepositoryBuilder GetExpiredOrCloseToDueDate(IList<MyFood> foods)
        {
            _repository.Setup(x => x.GetExpiredOrCloseToDueDate()).ReturnsAsync(foods);
            return this;
        }

        public MyFoodsReadOnlyRepositoryBuilder GetById(long userId, MyFood myFood)
        {
            _repository.Setup(x => x.GetById(myFood.Id, userId)).ReturnsAsync(myFood);
            return this;
        }

        public MyFoodsReadOnlyRepositoryBuilder GetByUserId(long userId, IList<MyFood> myFoods)
        {
            _repository.Setup(x => x.GetByUserId(userId)).ReturnsAsync(myFoods);
            return this;
        }

        public IMyFoodsReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
