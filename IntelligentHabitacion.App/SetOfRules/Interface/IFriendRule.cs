using IntelligentHabitacion.App.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Interface
{
    public interface IFriendRule
    {
        Task<List<FriendModel>> GetHouseFriends();
        Task<FriendModel> ChangeDateJoinOn(string friendId, DateTime date);
        Task NotifyFriendOrderHasArrived(string friendId);
        Task RequestCodeToChangeAdministrator();
        Task ChangeAdministrator(string code, string friendId, string password);
    }
}
