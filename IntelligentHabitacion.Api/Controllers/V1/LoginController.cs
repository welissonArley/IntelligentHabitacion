using IntelligentHabitacion.Api.SetOfRules.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Login()
        {
            try
            {
                return Ok();
            }
            catch (Exception exception)
            {
                return TratarException(exception);
            }
        }
    }
}