using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.Home.RegisterHome
{
    public interface IRegisterHomeUseCase
    {
        Task<ResponseOutput> Execute(RequestRegisterHomeJson registerHomeJson);
    }
}
