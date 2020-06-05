using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.Services.Interface;
using Microsoft.AspNetCore.Http;

namespace IntelligentHabitacion.Api.SetOfRules.LoggedUser
{
    public class LoggedUser : ILoggedUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly ITokenController _tokenController;
        private User user;

        public LoggedUser(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, ITokenController tokenController)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _tokenController = tokenController;
            user = null;
        }

        public User User()
        {
            if (user != null)
                return user;

            var autorizacao = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            var token = autorizacao.Substring("Basic ".Length).Trim();

            var email = _tokenController.User(token);

            user = _userRepository.GetByEmail(email);

            return user;
        }
    }
}
