using IntelligentHabitacion.App.Model;
using System;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.Friends.ChangeDateFriendJoinHome
{
    public interface IChangeDateFriendJoinHomeUseCase
    {
        Task<FriendModel> Execute(string friendId, DateTime date);
    }
}
