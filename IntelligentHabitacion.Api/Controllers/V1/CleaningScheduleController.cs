using IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.CreateFirstSchedule;
using IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.GetTasks;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsAdminAttribute))]
        public async Task<IActionResult> CreateFirstCleaningSchedule([FromServices] ICreateFirstScheduleUseCase useCase,
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
        /*
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

        /// <summary>
        /// This function return one array with the all friend's tasks
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("FriendsTasks")]
        [ProducesResponseType(typeof(List<ResponseAllFriendsTasksScheduleJson>), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public async Task<IActionResult> GetFriendsTasks(
            [FromServices] IGetFriendsTasksUseCase useCase,
            [FromBody] RequestDateJson request)
        {
            try
            {
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
        /// One user can rate un friend's task completed and return the new average rating
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("RateTask/{id:hashids}")]
        [ProducesResponseType(typeof(ResponseAverageRatingJson), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public async Task<IActionResult> RateTaskCompleted(
            [FromServices] IRateTaskUseCase useCase,
            [FromBody] RequestRateTaskJson request,
            [FromRoute][ModelBinder(typeof(Binder.HashidsModelBinder))] long id)
        {
            try
            {
                VerifyParameters(request);

                var response = await useCase.Execute(id, request);
                WriteAutenticationHeader(response);

                return Ok(response.ResponseJson);
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function will return one list with all task's rate
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Feedbacks/{id:hashids}")]
        [ProducesResponseType(typeof(ResponseRateTaskJson), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public async Task<IActionResult> GetFeedbacks(
            [FromServices] IGetTaskFeedbacksUseCase useCase,
            [FromRoute][ModelBinder(typeof(Binder.HashidsModelBinder))] long id)
        {
            try
            {
                var response = await useCase.Execute(id);
                WriteAutenticationHeader(response);

                return Ok(response.ResponseJson);
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }*/
    }
}
