using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.Home.HomeInformations
{
    public interface IHomeInformationsUseCase
    {
        Task<ResponseOutput> Execute();
    }
}
