using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.DeleteMyFood
{
    public interface IDeleteMyFoodUseCase
    {
        Task<ResponseOutput> Execute(long myFoodId);
    }
}
