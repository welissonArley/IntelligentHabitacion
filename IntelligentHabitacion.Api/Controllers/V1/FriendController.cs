using IntelligentHabitacion.Api.Filter;
using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IntelligentHabitacion.Api.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FriendController : BaseController
    {
        private readonly IFriendRule _friendRule;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="friendRule"></param>
        public FriendController(IFriendRule friendRule)
        {
            _friendRule = friendRule;
        }

        /// <summary>
        /// This function will return the list of friends
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Friends")]
        [ProducesResponseType(typeof(List<ResponseFriendJson>), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public IActionResult Friends()
        {
            try
            {
                var list = _friendRule.GetFriends();
                if (list.Count == 0)
                    return NoContent();

                return Ok(list);
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function will change the date when the friend joined at home
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ChangeDateJoinHome")]
        [ProducesResponseType(typeof(ResponseFriendJson), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsAdminAttribute))]
        public IActionResult ChangeDateJoinHome(RequestChangeDateJoinHomeJson request)
        {
            try
            {
                VerifyParameters(request);
                var response = _friendRule.ChangeDateJoinHome(request);

                return Ok(response);
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function will notify one friend that one order has arrived
        /// </summary>
        /// <param name="friendId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("NotifyOrderReceived")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public IActionResult NotifyOrderReceived(string friendId)
        {
            try
            {
                VerifyParameters(friendId);
                _friendRule.NotifyOrderHasArrived(friendId);

                return Ok();
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function will send one code to e-mail of the house's Administrator
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("RequestCodeChangeAdministrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsAdminAttribute))]
        public IActionResult RequestCodeChangeAdministrator()
        {
            try
            {
                _friendRule.RequestCodeChangeAdministrator();

                return Ok();
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function will change the house's Administrator
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ChangeAdministrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsAdminAttribute))]
        public IActionResult ChangeAdministrator(RequestAdminActionsOnFriendJson request)
        {
            try
            {
                VerifyParameters(request);
                _friendRule.ChangeAdministrator(request);

                return Ok();
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function will send one code to e-mail of the house's Administrator
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("RequestCodeRemoveFriend")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsAdminAttribute))]
        public IActionResult RequestCodeRemoveFriend()
        {
            try
            {
                _friendRule.RequestCodeRemoveFriend();

                return Ok();
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function will remove the friend
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RemoveFriend")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsAdminAttribute))]
        public IActionResult RemoveFriend(RequestAdminActionsOnFriendJson request)
        {
            try
            {
                VerifyParameters(request);
                _friendRule.RemoveFriend(request);

                return Ok();
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }
    }
}