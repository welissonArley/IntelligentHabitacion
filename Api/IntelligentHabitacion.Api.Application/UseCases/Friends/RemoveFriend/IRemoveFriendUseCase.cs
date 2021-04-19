using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.Friends.RemoveFriend
{
    public interface IRemoveFriendUseCase
    {
        Task<ResponseOutput> Execute(long friendId, RequestAdminActionJson request);
    }
}
