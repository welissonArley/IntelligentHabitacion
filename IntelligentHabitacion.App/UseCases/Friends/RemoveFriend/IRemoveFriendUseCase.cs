using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.Friends.RemoveFriend
{
    public interface IRemoveFriendUseCase
    {
        Task Execute(string friendId, string code, string password);
    }
}
