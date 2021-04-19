using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.MyFoods.UpdateMyFood
{
    public interface IUpdateMyFoodUseCase
    {
        Task<ResponseOutput> Execute(long myFoodId, RequestProductJson editMyFood);
    }
}
