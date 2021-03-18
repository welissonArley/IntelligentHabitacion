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
    }
}
