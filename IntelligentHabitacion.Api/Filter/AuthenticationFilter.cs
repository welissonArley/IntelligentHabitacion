using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Token;
using IntelligentHabitacion.Api.SetOfRules.JWT;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace IntelligentHabitacion.Api.Filter
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationFilter : Attribute, IActionFilter
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository"></param>
        public AuthenticationFilter(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
                    var tokenController = new TokenController();
                    var email = tokenController.User(tokenRequest);
                    var user = _userRepository.GetByEmail(email);

                    if (user == null)
                        UserDoesNotHaveAccess(context);

                    var token = new TokenRepository().Get(user.Id);
                    if (!token.Value.Equals(tokenRequest))
                        UserDoesNotHaveAccess(context);
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
