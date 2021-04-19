using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.MyFoods.GetMyFoods
{
    public interface IGetMyFoodsUseCase
    {
        Task<ResponseOutput> Execute();
    }
}
