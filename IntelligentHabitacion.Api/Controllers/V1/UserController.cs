using IntelligentHabitacion.Api.Application.UseCases.RegisterUser;
using IntelligentHabitacion.Api.Filter;
using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Communication.Boolean;
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
    public class UserController : BaseController
    {
        private readonly IUserRule _userRule;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRule"></param>
        public UserController(IUserRule userRule)
        {
            _userRule = userRule;
        }

        /// <summary>
        /// This function verify if the user's informations is correct and save the informations on database
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="registerUserJson"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("Register")]
        public IActionResult Register(
            [FromServices] IRegisterUserUseCase useCase,
            [FromBody] RequestRegisterUserJson registerUserJson)
        {
            try
            {
                VerifyParameters(registerUserJson);

                var profileColor = useCase.Execute(registerUserJson);
                return Created(string.Empty, profileColor);
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function verify if the e-mail address has already been registered.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("EmailAlreadyBeenRegistered/{email}")]
        [ProducesResponseType(typeof(BooleanJson), StatusCodes.Status200OK)]
        public IActionResult EmailAlreadyBeenRegistered(string email)
        {
            try
            {
                VerifyParameters(email);

                var response = _userRule.EmailAlreadyBeenRegistered(email);
                return Ok(response);
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function will update the logged user's personal informations
        /// </summary>
        /// <param name="updateUserJson"></param>
        /// <returns></returns>
        [HttpPut]
        [ServiceFilter(typeof(AuthenticationUserAttribute))]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Update(RequestUpdateUserJson updateUserJson)
        {
            try
            {
                VerifyParameters(updateUserJson);

                _userRule.Update(updateUserJson);
                return Ok();
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function will update the password
        /// </summary>
        /// <param name="changePasswordJson"></param>
        /// <returns></returns>
        [HttpPut]
        [ServiceFilter(typeof(AuthenticationUserAttribute))]
        [Route("ChangePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ChangePassword(RequestChangePasswordJson changePasswordJson)
        {
            try
            {
                VerifyParameters(changePasswordJson);
                _userRule.ChangePassword(changePasswordJson);
                return Ok();
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function will return the user's informations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ServiceFilter(typeof(AuthenticationUserAttribute))]
        [Route("Informations")]
        [ProducesResponseType(typeof(ResponseUserInformationsJson), StatusCodes.Status200OK)]
        public IActionResult Informations()
        {
            try
            {
                var informations = _userRule.GetInformations();
                return Ok(informations);
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }
    }
}