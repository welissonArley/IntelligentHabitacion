using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.Friends.ChangeAdministrator
{
    public interface IChangeAdministratorUseCase
    {
        Task<ResponseOutput> Execute(long friendId, RequestAdminActionJson request);
    }
}
