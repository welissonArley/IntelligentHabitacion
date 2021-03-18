using IntelligentHabitacion.App.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.Friends.GetMyFriends
{
    public interface IGetMyFriendsUseCase
    {
        Task<IList<FriendModel>> Execute();
    }
}
