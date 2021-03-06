using IntelligentHabitacion.Api.Application.UseCases.GetCleaningSchedule;
using IntelligentHabitacion.Api.Application.UseCases.GetMyTasksCleaningSchedule;
using IntelligentHabitacion.Api.Application.UseCases.GetUsersTaskDetails;
using IntelligentHabitacion.Api.Application.UseCases.TaskCompletedToday;
using IntelligentHabitacion.Api.Application.UseCases.UpdateCleaningSchedule;
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
        /// If the cleaning schedule dont exist, this function will return an another objet with the message and the action to do
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MyTasks")]
        [ProducesResponseType(typeof(ResponseMyTasksCleaningScheduleJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseNeedActionJson), StatusCodes.Status206PartialContent)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public async Task<IActionResult> MyTasks([FromServices] IGetMyTasksCleaningScheduleUseCase useCase,
            [FromBody] RequestDateJson request)
        {
            try
            {
                VerifyParameters(request);

                var response = await useCase.Execute(request.Date);
                WriteAutenticationHeader(response);

                if (response.ResponseJson as ResponseNeedActionJson is null)
                    return Ok(response.ResponseJson);

                return StatusCode(StatusCodes.Status206PartialContent, response.ResponseJson);
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function will return an object with the current cleaning schedule.
        /// </summary>
        /// <param name="useCase"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CleaningSchedule")]
        [ProducesResponseType(typeof(ResponseManageScheduleJson), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsAdminAttribute))]
        public async Task<IActionResult> GetCleaningSchedule([FromServices] IGetCleaningScheduleUseCase useCase)
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

        /// <summary>
        /// This function will update the current cleaning schedule
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CleaningSchedule")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsAdminAttribute))]
        public async Task<IActionResult> UpdateCleaningSchedule([FromServices] IUpdateCleaningScheduleUseCase useCase,
            [FromBody] List<RequestUpdateCleaningScheduleJson> request)
        {
            try
            {
                VerifyParameters(request);

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
        /// This function will save one register to confirm that the user cleaned today the room received as parameter
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("TaskCompleted/{id:hashids}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public async Task<IActionResult> TaskCompletedToday(
            [FromServices] ITaskCompletedTodayUseCase useCase,
            [FromRoute][ModelBinder(typeof(Binder.HashidsModelBinder))] long id)
        {
            try
            {
                var response = await useCase.Execute(id);
                WriteAutenticationHeader(response);

                return Ok();
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function will return the object with datails to each task of a user
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UserTaskDetails/{id:hashids}")]
        [ProducesResponseType(typeof(ResponseDetailsUserScheduleJson), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public async Task<IActionResult> GetUsersTaskDetails(
            [FromServices] IGetUsersTaskDetailsUseCase useCase,
            [FromBody] RequestDateJson request,
            [FromRoute][ModelBinder(typeof(Binder.HashidsModelBinder))] long id)
        {
            try
            {
                var response = await useCase.Execute(id, request.Date);
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
