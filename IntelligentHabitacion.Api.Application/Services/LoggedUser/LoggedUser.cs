using IntelligentHabitacion.Api.Application.Services.Token;
using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository.User;
using Microsoft.AspNetCore.Http;

namespace IntelligentHabitacion.Api.Application.Services.LoggedUser
{
    public class LoggedUser : ILoggedUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserReadOnlyRepository _repository;
        private readonly TokenController _tokenController;
        private User user;

        public LoggedUser(IHttpContextAccessor httpContextAccessor,
            IUserReadOnlyRepository repository,
            TokenController tokenController)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _tokenController = tokenController;
            user = null;
        }

        public User User()
        {
            if (user != null)
                return user;

            var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            var token = authorization.Substring("Basic ".Length).Trim();

            var email = _tokenController.User(token);

            user = _repository.GetByEmail(email);

            return user;
        }
    }
}
