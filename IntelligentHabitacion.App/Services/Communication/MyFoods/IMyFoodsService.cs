using IntelligentHabitacion.Communication.Request;
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
        [Post("/AddFood")]
        Task<ApiResponse<string>> AddMyFood([Body] RequestProductJson myFood, [Authorize("Basic")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Put("/ChangeQuantity/{myFoodId}")]
        Task<ApiResponse<string>> ChangeQuantityMyFood(string myFoodId, [Body] RequestChangeQuantityMyFoodJson myFood, [Authorize("Basic")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
    }
}
