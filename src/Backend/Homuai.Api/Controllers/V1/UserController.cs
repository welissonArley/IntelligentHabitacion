using Homuai.Application.UseCases.User.RegisterUser;
using Homuai.Communication.Request;
using Homuai.Communication.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Homuai.Api.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : BaseController
    {
        /// <summary>
        /// This function verify if the user's informations is correct and save the informations on database
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="registerUserJson"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseUserRegisteredJson), StatusCodes.Status201Created)]
        [Route("register")]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterUserUseCase useCase,
            [FromBody] RequestRegisterUserJson registerUserJson)
        {
            var response = await useCase.Execute(registerUserJson);

            WriteAutenticationHeader(response);
            return Created(string.Empty, response.ResponseJson);
        }
    }
}
