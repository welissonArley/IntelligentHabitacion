using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.ChangeAdministrator
{
    public interface IChangeAdministratorUseCase
    {
        Task<ResponseOutput> Execute(long friendId, RequestAdminActionJson request);
    }
}
