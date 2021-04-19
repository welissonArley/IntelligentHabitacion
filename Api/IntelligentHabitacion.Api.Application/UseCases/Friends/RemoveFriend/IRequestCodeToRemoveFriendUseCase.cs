using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.Friends.RemoveFriend
{
    public interface IRequestCodeToRemoveFriendUseCase
    {
        Task<ResponseOutput> Execute();
    }
}
