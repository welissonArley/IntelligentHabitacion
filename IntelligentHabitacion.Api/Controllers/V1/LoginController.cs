using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntelligentHabitacion.Api.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LoginController : BaseController
    {
        private readonly ILoginRule _loginRule;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginRule"></param>
        public LoginController(ILoginRule loginRule)
        {
            _loginRule = loginRule;
        }

        /// <summary>
        /// Function to do Login on API
        /// </summary>
        /// <param name="loginJson"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseLoginJson), StatusCodes.Status200OK)]
        public IActionResult Login(RequestLoginJson loginJson)
        {
            try
            {
                VerifyParameters(loginJson);
                var response = _loginRule.DoLogin(loginJson);
                return Ok(response);
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// Use this function to get a code to reset your password
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("RequestCodeResetPassword/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult RequestCodeResetPassword(string email)
        {
            try
            {
                VerifyParameters(email);
                _loginRule.RequestCodeToResetPassword(email);
                return Ok();
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// Use this function to reset the password
        /// </summary>
        /// <param name="resetYourPasswordJson"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ResetYourPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ResetYourPassword(RequestResetYourPasswordJson resetYourPasswordJson)
        {
            try
            {
                VerifyParameters(resetYourPasswordJson);
                _loginRule.ResetYourPassword(resetYourPasswordJson);
                return Ok();
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }
    }
}