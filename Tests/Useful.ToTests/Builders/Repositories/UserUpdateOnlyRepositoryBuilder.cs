using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository.User;
using Moq;

namespace Useful.ToTests.Builders.Repositories
{
    public class UserUpdateOnlyRepositoryBuilder
    {
        private static UserUpdateOnlyRepositoryBuilder _instance;
        private readonly Mock<IUserUpdateOnlyRepository> _repository;

        private UserUpdateOnlyRepositoryBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<IUserUpdateOnlyRepository>();
            }
        }

        public static UserUpdateOnlyRepositoryBuilder Instance()
        {
            _instance = new UserUpdateOnlyRepositoryBuilder();
            return _instance;
        }

        public UserUpdateOnlyRepositoryBuilder GetByEmail(string email, User user)
        {
            _repository.Setup(x => x.GetByEmail_Update(email)).ReturnsAsync(user);
            return this;
        }

        public IUserUpdateOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
