using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Communication.Request;
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
        /// <param name="registerUserJson"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Register(RequestRegisterUserJson registerUserJson)
        {
            try
            {
                VerifyParameters(registerUserJson);

                _userRule.Register(registerUserJson);
                return Created(string.Empty, string.Empty);
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }
    }
}