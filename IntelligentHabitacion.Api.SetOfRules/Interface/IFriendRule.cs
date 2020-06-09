using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using System.Collections.Generic;

namespace IntelligentHabitacion.Api.SetOfRules.Interface
{
    public interface IFriendRule
    {
        List<ResponseFriendJson> GetFriends();
        ResponseFriendJson ChangeDateJoinHome(RequestChangeDateJoinHomeJson request);
        void NotifyOrderHasArrived(string friendId);
        void ChangeAdministrator(RequestAdminActionsOnFriendJson request);
        void RequestCodeChangeAdministrator();
        void RequestCodeRemoveFriend();
        void RemoveFriend(RequestAdminActionsOnFriendJson request);
    }
}
