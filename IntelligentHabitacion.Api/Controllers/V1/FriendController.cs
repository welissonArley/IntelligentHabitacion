using IntelligentHabitacion.Api.Filter;
using IntelligentHabitacion.Api.SetOfRules.Interface;
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
    [ServiceFilter(typeof(AuthenticationUserAttribute))]
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
    }
}