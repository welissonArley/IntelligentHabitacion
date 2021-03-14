using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using Refit;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.Services.Communication.Login
{
    [Headers("Content-Type: application/json")]
    public interface ILoginRestService
    {
        [Post("")]
        Task<ApiResponse<ResponseLoginJson>> DoLogin([Body] RequestLoginJson loggin, [Header("Accept-Language")] StringWithQualityHeaderValue language);
    }
}
