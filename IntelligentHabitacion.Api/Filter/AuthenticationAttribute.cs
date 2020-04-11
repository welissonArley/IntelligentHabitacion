using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Token;
using IntelligentHabitacion.Api.SetOfRules.Token;
using IntelligentHabitacion.Communication.Error;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;

namespace IntelligentHabitacion.Api.Filter
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationAttribute : Attribute, IActionFilter
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly ITokenController _tokenController;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="tokenRepository"></param>
        /// <param name="tokenController"></param>
        public AuthenticationAttribute(IUserRepository userRepository, ITokenRepository tokenRepository, ITokenController tokenController)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _tokenController = tokenController;
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
            var authentication = context.HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authentication))
                UserDoesNotHaveAccess(context);
            else
            {
                try
                {
                    var tokenRequest = authentication.Substring("Basic ".Length).Trim();
                    var email = _tokenController.User(tokenRequest);
                    var user = _userRepository.GetByEmail(email);

                    if (user == null)
                        UserDoesNotHaveAccess(context);
                    else
                    {
                        var token = _tokenRepository.Get(user.Id);
                        if (!token.Value.Equals(tokenRequest))
                            UserDoesNotHaveAccess(context);
                    }
                }
                catch(SecurityTokenExpiredException)
                {
                    context.Result = new UnauthorizedObjectResult(new ErrorJson
                    {
                        ErrorCode = ErrorCode.TokenExpired
                    });
                }
                catch
                {
                    UserDoesNotHaveAccess(context);
                }
            }
        }

        private void UserDoesNotHaveAccess(ActionExecutingContext context)
        {
            context.Result = new UnauthorizedObjectResult(new IntelligentHabitacionException(ResourceTextException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE));
        }
    }
}
