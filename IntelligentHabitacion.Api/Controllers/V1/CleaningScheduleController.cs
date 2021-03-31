using IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.CreateFirstSchedule;
using IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.GetTasks;
using IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.RegisterRoomCleaned;
using IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.Reminder;
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
    public class CleaningScheduleController : BaseController
    {
        /// <summary>
        /// This function will return an object with the user's tasks for the date.
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Tasks")]
        [ProducesResponseType(typeof(ResponseTasksJson), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public async Task<IActionResult> MyTasks([FromServices] IGetTasksUseCase useCase,
            [FromBody] RequestDateJson request)
        {
            try
            {
                VerifyParameters(request);

                var response = await useCase.Execute(request.Date);
                WriteAutenticationHeader(response);

                return Ok(response.ResponseJson);
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function will create only the first cleaning schedule
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CleaningSchedule")]
        [ProducesResponseType(typeof(ResponseScheduleTasksCleaningHouseJson), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsAdminAttribute))]
        public async Task<IActionResult> CreateFirstCleaningSchedule([FromServices] ICreateFirstScheduleUseCase useCase,
            [FromBody] List<RequestUpdateCleaningScheduleJson> request)
        {
            try
            {
                VerifyParameters(request);

                var response = await useCase.Execute(request);
                WriteAutenticationHeader(response);

                return Ok(response.ResponseJson);
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function will save one register to confirm that the user cleaned the room received as parameter
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("RegisterRoomCleaned")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public async Task<IActionResult> RegisterRoomCleaned(
            [FromServices] IRegisterRoomCleanedUseCase useCase,
            [FromBody] RequestRegisterRoomCleaned request)
        {
            try
            {
                var response = await useCase.Execute(request);
                WriteAutenticationHeader(response);

                return Ok();
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function will send a PushNotification to the users received as parameter to remember clean room
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Reminder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public async Task<IActionResult> Reminder(
            [FromServices] IReminderUseCase useCase,
            [FromBody] IList<string> request)
        {
            try
            {
                var response = await useCase.Execute(request);
                WriteAutenticationHeader(response);

                return Ok();
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }
    }
}
