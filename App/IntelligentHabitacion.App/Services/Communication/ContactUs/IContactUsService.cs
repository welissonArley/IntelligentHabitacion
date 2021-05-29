using IntelligentHabitacion.Communication.Request;
using Refit;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.Services.Communication.ContactUs
{
    public interface IContactUsService
    {
        [Post("")]
        Task<ApiResponse<string>> SendMessage([Body] RequestContactUsJson request, [Authorize("Basic")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
    }
}
