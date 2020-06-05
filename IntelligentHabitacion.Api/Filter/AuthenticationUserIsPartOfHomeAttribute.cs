using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Token;
using IntelligentHabitacion.Api.Services.Interface;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace IntelligentHabitacion.Api.Filter
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationUserIsPartOfHomeAttribute : AuthenticationBaseAttribute, IActionFilter
    {
        private readonly ITokenRepository _tokenRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="tokenRepository"></param>
        /// <param name="tokenController"></param>
        public AuthenticationUserIsPartOfHomeAttribute(IUserRepository userRepository, ITokenRepository tokenRepository, ITokenController tokenController) : base(userRepository, tokenController)
        {
            _tokenRepository = tokenRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //It's not necessary this action
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var user = GetUser(context);
                if (user == null || user.HomeAssociationId == null)
                    UserDoesNotHaveAccess(context);
                else
                {
                    var token = _tokenRepository.Get(user.Id);
                    var tokenRequest = TokenOnRequest(context);
                    if (!token.Value.Equals(tokenRequest))
                        UserDoesNotHaveAccess(context);
                }
            }
            catch (SecurityTokenExpiredException)
            {
                TokenExpired(context);
            }
            catch
            {
                UserDoesNotHaveAccess(context);
            }
        }
    }
}
