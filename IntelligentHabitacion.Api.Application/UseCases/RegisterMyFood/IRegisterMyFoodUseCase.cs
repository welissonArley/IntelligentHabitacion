using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.RegisterMyFood
{
    public interface IRegisterMyFoodUseCase
    {
        Task<ResponseOutput> Execute(RequestAddMyFoodJson requestMyFood);
    }
}
