using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.HomeInformations
{
    public interface IHomeInformationsUseCase
    {
        Task<ResponseOutput> Execute();
    }
}
