using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.RemoveFriend
{
    public interface IRemoveFriendUseCase
    {
        Task<ResponseOutput> Execute(long friendId, RequestAdminActionJson request);
    }
}
