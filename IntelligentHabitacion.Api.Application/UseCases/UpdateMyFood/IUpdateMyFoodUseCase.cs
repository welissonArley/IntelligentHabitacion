using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.UpdateMyFood
{
    public interface IUpdateMyFoodUseCase
    {
        Task<ResponseOutput> Execute(long myFoodId, RequestProductJson editMyFood);
    }
}
