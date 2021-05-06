using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Entity;
using Moq;

namespace Useful.ToTests.Builders.LoggedUser
{
    public class LoggedUserBuilder
    {
        private static LoggedUserBuilder _instance;
        private readonly Mock<ILoggedUser> _repository;

        private LoggedUserBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<ILoggedUser>();
            }
        }

        public static LoggedUserBuilder Instance()
        {
            _instance = new LoggedUserBuilder();
            return _instance;
        }

        public LoggedUserBuilder User(User user)
        {
            _repository.Setup(x => x.User()).ReturnsAsync(user);
            return this;
        }

        public ILoggedUser Build()
        {
            return _repository.Object;
        }
    }
}
