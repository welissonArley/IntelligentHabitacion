using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using Refit;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.Services.Communication.Friend
{
    [Headers("Content-Type: application/json")]
    public interface IFriendService
    {
        [Get("/Friends")]
        Task<ApiResponse<List<ResponseFriendJson>>> GetHouseFriends([Authorize("Basic")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Post("/NotifyOrderReceived/{friendId}")]
        Task<ApiResponse<string>> NotifyFriendOrderHasArrived(string friendId, [Authorize("Basic")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Put("/ChangeDateJoinHome/{friendId}")]
        Task<ApiResponse<ResponseFriendJson>> ChangeDateJoinHome(string friendId, [Body] RequestDateJson request, [Authorize("Basic")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Get("/RequestCodeRemoveFriend")]
        Task<ApiResponse<string>> RequestCodeToRemoveFriend([Authorize("Basic")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Post("/RemoveFriend/{friendId}")]
        Task<ApiResponse<string>> RemoveFriend(string friendId, [Body] RequestAdminActionJson request, [Authorize("Basic")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
    }
}
