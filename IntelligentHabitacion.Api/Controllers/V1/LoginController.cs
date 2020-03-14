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
        /// 
        /// </summary>
        /// <param name="loginJson"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseLoginJson), StatusCodes.Status200OK)]
        public IActionResult Login(RequestLoginJson loginJson)
        {
            try
            {
                var response = _loginRule.DoLogin(loginJson);
                return Ok(response);
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }
    }
}