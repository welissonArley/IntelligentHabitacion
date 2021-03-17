using IntelligentHabitacion.Communication.Response;
using Refit;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.Services.Communication.MyFoods
{
    public interface IMyFoodsService
    {
        [Get("/MyFoods")]
        Task<ApiResponse<List<ResponseMyFoodJson>>> GetMyFoods([Authorize("Basic")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
    }
}
