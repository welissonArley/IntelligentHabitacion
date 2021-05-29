using IntelligentHabitacion.Api.Application.UseCases.Friends.ChangeAdministrator;
using IntelligentHabitacion.Api.Application.UseCases.Friends.ChangeDateFriendJoinHome;
using IntelligentHabitacion.Api.Application.UseCases.Friends.GetMyFriends;
using IntelligentHabitacion.Api.Application.UseCases.Friends.NotifyOrderReceived;
using IntelligentHabitacion.Api.Application.UseCases.Friends.RemoveFriend;
using IntelligentHabitacion.Api.Binder;
using IntelligentHabitacion.Api.Filter;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FriendController : BaseController
    {
        /// <summary>
        /// This function will return the list of friends
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Friends")]
        [ProducesResponseType(typeof(List<ResponseFriendJson>), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public async Task<IActionResult> Friends([FromServices] IGetMyFriendsUseCase useCase)
        {
            var response = await useCase.Execute();
            WriteAutenticationHeader(response);

            return Ok(response.ResponseJson);
        }

        /// <summary>
        /// This function will change the date when the friend joined at home
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ChangeDateJoinHome/{id:hashids}")]
        [ProducesResponseType(typeof(ResponseFriendJson), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsAdminAttribute))]
        public async Task<IActionResult> ChangeDateJoinHome(
            [FromServices] IChangeDateFriendJoinHomeUseCase useCase,
            [FromRoute][ModelBinder(typeof(HashidsModelBinder))] long id,
            [FromBody] RequestDateJson request)
        {
            VerifyParameters(request);

            var response = await useCase.Execute(id, request);
            WriteAutenticationHeader(response);

            return Ok(response.ResponseJson);
        }

        /// <summary>
        /// This function will notify one friend that one order has arrived
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("NotifyOrderReceived/{id:hashids}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public async Task<IActionResult> NotifyOrderReceived(
            [FromServices] INotifyOrderReceivedUseCase useCase,
            [FromRoute][ModelBinder(typeof(HashidsModelBinder))] long id)
        {
            var response = await useCase.Execute(id);
            WriteAutenticationHeader(response);

            return Ok();
        }

        /// <summary>
        /// This function will send one code to e-mail of the house's Administrator
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("RequestCodeChangeAdministrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsAdminAttribute))]
        public async Task<IActionResult> RequestCodeChangeAdministrator([FromServices] IRequestCodeChangeAdministratorUseCase useCase)
        {
            var response = await useCase.Execute();
            WriteAutenticationHeader(response);

            return Ok();
        }

        /// <summary>
        /// This function will change the house's Administrator
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ChangeAdministrator/{id:hashids}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsAdminAttribute))]
        public async Task<IActionResult> ChangeAdministrator(
            [FromServices] IChangeAdministratorUseCase useCase,
            [FromRoute][ModelBinder(typeof(HashidsModelBinder))] long id,
            [FromBody] RequestAdminActionJson request)
        {
            VerifyParameters(request);

            var response = await useCase.Execute(id, request);
            WriteAutenticationHeader(response);

            return Ok();
        }

        /// <summary>
        /// This function will send one code to e-mail of the house's Administrator
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("RequestCodeRemoveFriend")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsAdminAttribute))]
        public async Task<IActionResult> RequestCodeRemoveFriend([FromServices] IRequestCodeToRemoveFriendUseCase useCase)
        {
            var response = await useCase.Execute();
            WriteAutenticationHeader(response);

            return Ok();
        }

        /// <summary>
        /// This function will remove the friend
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RemoveFriend/{id:hashids}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsAdminAttribute))]
        public async Task<IActionResult> RemoveFriend(
            [FromServices] IRemoveFriendUseCase useCase,
            [FromRoute][ModelBinder(typeof(HashidsModelBinder))] long id,
            [FromBody] RequestAdminActionJson request)
        {
            VerifyParameters(request);

            var response = await useCase.Execute(id, request);
            WriteAutenticationHeader(response);

            return Ok();
        }
    }
}