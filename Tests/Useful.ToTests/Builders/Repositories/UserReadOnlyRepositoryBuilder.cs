using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository.User;
using Moq;

namespace Useful.ToTests.Builders.Repositories
{
    public class UserReadOnlyRepositoryBuilder
    {
        private static UserReadOnlyRepositoryBuilder _instance;
        private readonly Mock<IUserReadOnlyRepository> _repository;

        private UserReadOnlyRepositoryBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<IUserReadOnlyRepository>();
            }
        }

        public static UserReadOnlyRepositoryBuilder Instance()
        {
            _instance = new UserReadOnlyRepositoryBuilder();
            return _instance;
        }

        public UserReadOnlyRepositoryBuilder GetByEmail(User user)
        {
            _repository.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);
            return this;
        }

        public UserReadOnlyRepositoryBuilder GetById(User user)
        {
            _repository.Setup(x => x.GetById(user.Id)).ReturnsAsync(user);
            return this;
        }

        public UserReadOnlyRepositoryBuilder ExistActiveUserWithEmail(string email)
        {
            _repository.Setup(x => x.ExistActiveUserWithEmail(email)).ReturnsAsync(true);
            return this;
        }

        public UserReadOnlyRepositoryBuilder GetByEmailPassword(string email, string password)
        {
            _repository.Setup(x => x.GetByEmailPassword(email, password)).ReturnsAsync(new User
            {
                Email = email,
                Password = password,
                Active = true,
                Name = "User",
                ProfileColor = "#000000"
            });

            return this;
        }

        public IUserReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
