using IntelligentHabitacion.Api.Application.UseCases.ForgotPassword;
using IntelligentHabitacion.Api.Application.UseCases.Login;
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
        /// <param name="useCase"></param>
        /// <param name="loginJson"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseLoginJson), StatusCodes.Status200OK)]
        public IActionResult Login(
            [FromServices] ILoginUseCase useCase,
            [FromBody] RequestLoginJson loginJson)
        {
            try
            {
                VerifyParameters(loginJson);
                
                var response = useCase.Execute(loginJson);
                WriteAutenticationHeader(response);

                return Ok(response.ResponseJson);
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
        public IActionResult RequestCodeResetPassword(
            [FromServices] IRequestCodeResetPasswordUseCase useCase,
            [FromRoute] string email)
        {
            try
            {
                VerifyParameters(email);
                useCase.Execute(email);

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
        /// <param name="useCase"></param>
        /// <param name="resetYourPasswordJson"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ResetYourPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ResetYourPassword(
            [FromServices] IResetPasswordUseCase useCase,
            [FromBody] RequestResetYourPasswordJson resetYourPasswordJson)
        {
            try
            {
                VerifyParameters(resetYourPasswordJson);

                useCase.Execute(resetYourPasswordJson);

                return Ok();
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }
    }
}