using IntelligentHabitacion.Api.Application.UseCases.ChangePassword;
using IntelligentHabitacion.Api.Application.UseCases.EmailAlreadyBeenRegistered;
using IntelligentHabitacion.Api.Application.UseCases.RegisterUser;
using IntelligentHabitacion.Api.Application.UseCases.UpdateUserInformations;
using IntelligentHabitacion.Api.Application.UseCases.UserInformations;
using IntelligentHabitacion.Api.Filter;
using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Communication.Boolean;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Register(
            [FromServices] IRegisterUserUseCase useCase,
            [FromBody] RequestRegisterUserJson registerUserJson)
        {
            try
            {
                VerifyParameters(registerUserJson);

                var response = await useCase.Execute(registerUserJson);

                WriteAutenticationHeader(response);
                return Created(string.Empty, response.ResponseJson);
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function verify if the e-mail address has already been registered.
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("EmailAlreadyBeenRegistered/{email}")]
        [ProducesResponseType(typeof(BooleanJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> EmailAlreadyBeenRegistered(
            [FromServices] IEmailAlreadyBeenRegisteredUseCase useCase,
            [FromRoute] string email)
        {
            try
            {
                VerifyParameters(email);

                var response = await useCase.Execute(email);
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
        /// <param name="useCase"></param>
        /// <param name="updateUserJson"></param>
        /// <returns></returns>
        [HttpPut]
        [ServiceFilter(typeof(AuthenticationUserAttribute))]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(
            [FromServices] IUpdateUserInformationsUseCase useCase,
            [FromBody] RequestUpdateUserJson updateUserJson)
        {
            try
            {
                VerifyParameters(updateUserJson);

                var response = await useCase.Execute(updateUserJson);
                WriteAutenticationHeader(response);

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
        /// <param name="useCase"></param>
        /// <param name="changePasswordJson"></param>
        /// <returns></returns>
        [HttpPut]
        [ServiceFilter(typeof(AuthenticationUserAttribute))]
        [Route("ChangePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangePassword(
            [FromServices] IChangePasswordUseCase useCase,
            [FromBody] RequestChangePasswordJson changePasswordJson)
        {
            try
            {
                VerifyParameters(changePasswordJson);
                var response = await useCase.Execute(changePasswordJson);

                WriteAutenticationHeader(response);
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
        public async Task<IActionResult> Informations([FromServices] IUserInformationsUseCase useCase)
        {
            try
            {
                var response = await useCase.Execute();
                WriteAutenticationHeader(response);

                return Ok(response.ResponseJson);
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }
    }
}