﻿using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.SetOfRules.Token;
using IntelligentHabitacion.Communication.Error;
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
    public class AuthenticationBaseAttribute : Attribute
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenController _tokenController;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="tokenController"></param>
        public AuthenticationBaseAttribute(IUserRepository userRepository, ITokenController tokenController)
        {
            _userRepository = userRepository;
            _tokenController = tokenController;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected void UserDoesNotHaveAccess(ActionExecutingContext context)
        {
            context.Result = new UnauthorizedObjectResult(new IntelligentHabitacionException(ResourceTextException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected void TokenExpired(ActionExecutingContext context)
        {
            context.Result = new UnauthorizedObjectResult(new ErrorJson
            {
                ErrorCode = ErrorCode.TokenExpired
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected User GetUser(ActionExecutingContext context)
        {
            var tokenRequest = TokenOnRequest(context);
            var email = _tokenController.User(tokenRequest);
            return _userRepository.GetByEmail(email);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected string TokenOnRequest(ActionExecutingContext context)
        {
            var authentication = context.HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authentication))
                throw new IntelligentHabitacionException("");

            return authentication.Substring("Basic ".Length).Trim();
        }
    }
}