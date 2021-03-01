using IntelligentHabitacion.Api.Application.UseCases.GetCleaningSchedule;
using IntelligentHabitacion.Api.Filter;
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
    public class CleaningScheduleController : BaseController
    {
        /// <summary>
        /// This function will return an object with the cleaning schedule for the data.
        /// If the cleaning schedule dont exist, this function will return an another objet with the message and the action to do
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Friends")]
        [ProducesResponseType(typeof(ResponseMyTasksCleaningCleanHouseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseNeedActionJson), StatusCodes.Status206PartialContent)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public async Task<IActionResult> GetMyCleaningSchedule([FromServices] IGetCleaningScheduleUseCase useCase,
            [FromBody] RequestDateJson date)
        {
            try
            {
                VerifyParameters(date);

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
