using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.GetMyFoods
{
    public interface IGetMyFoodsUseCase
    {
        Task<ResponseOutput> Execute();
    }
}
