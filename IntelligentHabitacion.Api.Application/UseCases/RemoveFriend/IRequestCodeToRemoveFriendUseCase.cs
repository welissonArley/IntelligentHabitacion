using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.RemoveFriend
{
    public interface IRequestCodeToRemoveFriendUseCase
    {
        Task<ResponseOutput> Execute();
    }
}
