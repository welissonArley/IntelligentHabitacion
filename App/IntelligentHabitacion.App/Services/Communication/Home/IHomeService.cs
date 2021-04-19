using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using Refit;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.Services.Communication.Home
{
    [Headers("Content-Type: application/json")]
    public interface IHomeService
    {
        [Post("/Register")]
        Task<ApiResponse<string>> CreateHome([Body] RequestRegisterHomeJson registerHome, [Authorize("Basic")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Get("/Informations")]
        Task<ApiResponse<ResponseHomeInformationsJson>> GetHomesInformations([Authorize("Basic")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Put("/Update")]
        Task<ApiResponse<string>> UpdateHome([Body] RequestUpdateHomeJson registerHome, [Authorize("Basic")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
    }
}
