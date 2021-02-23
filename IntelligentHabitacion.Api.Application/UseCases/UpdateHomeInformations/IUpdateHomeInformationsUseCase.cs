using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.UpdateHomeInformations
{
    public interface IUpdateHomeInformationsUseCase
    {
        Task<ResponseOutput> Execute(RequestUpdateHomeJson updateHomeJson);
    }
}
