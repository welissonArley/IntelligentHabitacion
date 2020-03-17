﻿using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.Repository.Token;
using IntelligentHabitacion.Api.SetOfRules.JWT;
using IntelligentHabitacion.Communication.Error;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using IntelligentHabitacion.Exception.Parameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NHibernate;
using System.IO;

namespace IntelligentHabitacion.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [ApiController]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override OkObjectResult Ok(object value)
        {
            WriteAutenticationHeader();
            return base.Ok(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override CreatedResult Created(string uri, object value)
        {
            WriteAutenticationHeader();
            return base.Created(uri, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        protected void VerifyParameters(object parameter)
        {
            var parameterIsString = parameter is string;
            if (parameterIsString)
            {
                if (string.IsNullOrWhiteSpace((string)parameter))
                    throw new ParametersEmptyOrNullException();
            }
            else if (parameter == null)
                throw new ParametersEmptyOrNullException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        protected ObjectResult HandleException(System.Exception exception)
        {
            Response.StatusCode = StatusCodes.Status500InternalServerError;
            WriteAutenticationHeader();

            if (!((exception as IntelligentHabitacionException) is null))
                return HandleIntelligentHabitacionException((IntelligentHabitacionException)exception);

            return ThrowUnknowError();
        }

        private ObjectResult HandleIntelligentHabitacionException(IntelligentHabitacionException exception)
        {
            if (!((exception as ErrorOnValidationException) is null))
            {
                ErrorOnValidationException validacaoException = (ErrorOnValidationException)exception;
                return BadRequest(CreateErrorJson(validacaoException));
            }
            else if (!((exception as NotFoundException) is null))
                return NotFound(CreateErrorJson(exception));
            else if (!((exception as InvalidLoginException) is null))
                return Unauthorized(CreateErrorJson(exception));

            return BadRequest(CreateErrorJson(exception));
        }

        private ErrorJson CreateErrorJson(IntelligentHabitacionException exception)
        {
            return new ErrorJson(exception.Message);
        }

        private ErrorJson CreateErrorJson(ErrorOnValidationException exception)
        {
            return new ErrorJson(exception.ErrorMensages);
        }

        private ObjectResult ThrowUnknowError()
        {
            return StatusCode(500, new ErrorJson(ResourceTextException.UNKNOW_ERROR));
        }

        private void WriteAutenticationHeader()
        {
            if (!ItIsNecessaryToGenerateToken())
                return;

            var tokenToUseInNextRequest = "";
            User user = null;

            if (Request.Path.Value.Contains("Login"))
            {
                var login = JsonConvert.DeserializeObject<RequestLoginJson>(GetBodyMessage());
                IUserRepository userRepository = (IUserRepository)HttpContext.RequestServices.GetService(typeof(IUserRepository));
                user = userRepository.GetUserByEmail(login.User);
                tokenToUseInNextRequest = new TokenController().CreateToken(user.Email);
            }
            else if (Request.Path.Value.Contains("User"))
            {
                var registerUser = JsonConvert.DeserializeObject<RequestRegisterUserJson>(GetBodyMessage());
                IUserRepository userRepository = (IUserRepository)HttpContext.RequestServices.GetService(typeof(IUserRepository));
                user = userRepository.GetUserByEmail(registerUser.Email);
                tokenToUseInNextRequest = new TokenController().CreateToken(user.Email);
            }

            CreateToken(user, tokenToUseInNextRequest);
            Response.Headers.Add("Tvih", tokenToUseInNextRequest);
        }

        private string GetBodyMessage()
        {
            var reader = new StreamReader(Request.Body);
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            return reader.ReadToEnd();
        }

        private void CreateToken(User user, string token)
        {
            ISessionFactory sessionFactory = (ISessionFactory)HttpContext.RequestServices.GetService(typeof(ISessionFactory));

            NHibernate.ISession Session = sessionFactory.OpenSession();

            ITransaction transaction = Session.BeginTransaction();

            var tokenRepository = new TokenRepository(Session);

            tokenRepository.Create(new Token
            {
                Value = token,
                User = user
            });

            transaction.Commit();
            Session.Close();
        }

        private bool ItIsNecessaryToGenerateToken()
        {
            if (Request.Path.Value.Contains("User/EmailAlreadyBeenRegistered/"))
                return false;

            if (Request.Path.Value.Contains("Login") && Response.StatusCode == StatusCodes.Status500InternalServerError)
                return false;

            if (Request.Path.Value.Contains("User") && Response.StatusCode == StatusCodes.Status500InternalServerError)
                return false;

            return true;
        }
    }
}
