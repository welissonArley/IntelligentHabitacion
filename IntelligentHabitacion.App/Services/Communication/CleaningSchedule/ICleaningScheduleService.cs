using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using Refit;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.Services.Communication.CleaningSchedule
{
    public interface ICleaningScheduleService
    {
        [Post("/Tasks")]
        Task<ApiResponse<ResponseTasksJson>> GetTasks([Body] RequestDateJson request, [Authorize("Basic")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Post("/CleaningSchedule")]
        Task<ApiResponse<ResponseScheduleTasksCleaningHouseJson>> CreateFirstCleaningSchedule([Body] IList<RequestUpdateCleaningScheduleJson> request, [Authorize("Basic")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Put("/RegisterRoomCleaned")]
        Task<ApiResponse<string>> RegisterRoomCleaned([Body] RequestRegisterRoomCleaned request, [Authorize("Basic")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Put("/Reminder")]
        Task<ApiResponse<string>> ReminderUsers([Body] IList<string> request, [Authorize("Basic")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Put("/Calendar")]
        Task<ApiResponse<ResponseCalendarCleaningScheduleJson>> Calendar([Body] RequestCalendarCleaningScheduleJson request, [Authorize("Basic")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Put("/HistoryOfTheDay")]
        Task<ApiResponse<IList<ResponseHistoryOfTheDayJson>>> HistoryOfTheDay([Body] RequestHistoryOfTheDayJson request, [Authorize("Basic")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Put("/EditTaskAssign")]
        Task<ApiResponse<string>> EditTaskAssign([Body] RequestEditAssignCleaningScheduleJson request, [Authorize("Basic")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
    }
}
