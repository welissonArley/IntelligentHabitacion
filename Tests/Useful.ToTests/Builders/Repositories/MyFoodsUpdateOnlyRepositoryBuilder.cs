using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository.MyFoods;
using Moq;

namespace Useful.ToTests.Builders.Repositories
{
    public class MyFoodsUpdateOnlyRepositoryBuilder
    {
        private static MyFoodsUpdateOnlyRepositoryBuilder _instance;
        private readonly Mock<IMyFoodsUpdateOnlyRepository> _repository;

        private MyFoodsUpdateOnlyRepositoryBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<IMyFoodsUpdateOnlyRepository>();
            }
        }

        public static MyFoodsUpdateOnlyRepositoryBuilder Instance()
        {
            _instance = new MyFoodsUpdateOnlyRepositoryBuilder();
            return _instance;
        }

        public MyFoodsUpdateOnlyRepositoryBuilder GetById(long userId, MyFood food)
        {
            _repository.Setup(c => c.GetById_Update(food.Id, userId)).ReturnsAsync(food);
            return this;
        }

        public IMyFoodsUpdateOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
