using IntelligentHabitacion.Api.Application.Services.Token;
using IntelligentHabitacion.Api.Domain.Repository.Token;
using IntelligentHabitacion.Api.Domain.Repository.User;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Filter
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationUserIsAdminAttribute : AuthenticationBaseAttribute, IAsyncAuthorizationFilter
    {
        private readonly ITokenReadOnlyRepository _tokenRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="tokenRepository"></param>
        /// <param name="tokenController"></param>
        public AuthenticationUserIsAdminAttribute(IUserReadOnlyRepository userRepository, ITokenReadOnlyRepository tokenRepository, TokenController tokenController) : base(userRepository, tokenController)
        {
            _tokenRepository = tokenRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var user = await GetUser(context);
                if (user == null || !user.HomeAssociationId.HasValue || user.IsAdministrator())
                    UserDoesNotHaveAccess(context);
                else
                {
                    var token = await _tokenRepository.GetByUserId(user.Id);
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
