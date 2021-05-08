using IntelligentHabitacion.Api.Domain.Repository.MyFoods;
using Moq;

namespace Useful.ToTests.Builders.Repositories
{
    public class MyFoodsWriteOnlyRepositoryBuilder
    {
        private static MyFoodsWriteOnlyRepositoryBuilder _instance;
        private readonly Mock<IMyFoodsWriteOnlyRepository> _repository;

        private MyFoodsWriteOnlyRepositoryBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<IMyFoodsWriteOnlyRepository>();
            }
        }

        public static MyFoodsWriteOnlyRepositoryBuilder Instance()
        {
            _instance = new MyFoodsWriteOnlyRepositoryBuilder();
            return _instance;
        }

        public IMyFoodsWriteOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
