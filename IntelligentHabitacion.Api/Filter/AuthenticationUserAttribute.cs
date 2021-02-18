using IntelligentHabitacion.Api.Application.Services.Token;
using IntelligentHabitacion.Api.Domain.Repository.Token;
using IntelligentHabitacion.Api.Domain.Repository.User;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace IntelligentHabitacion.Api.Filter
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationUserAttribute : AuthenticationBaseAttribute, IActionFilter
    {
        private readonly ITokenReadOnlyRepository _tokenRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="tokenRepository"></param>
        /// <param name="tokenController"></param>
        public AuthenticationUserAttribute(IUserReadOnlyRepository userRepository, ITokenReadOnlyRepository tokenRepository, TokenController tokenController) : base(userRepository, tokenController)
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
                if (user == null)
                    UserDoesNotHaveAccess(context);
                else
                {
                    var token = _tokenRepository.GetByUserId(user.Id).ConfigureAwait(false).GetAwaiter().GetResult();
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
