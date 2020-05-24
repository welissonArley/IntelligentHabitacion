using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using System.Collections.Generic;

namespace IntelligentHabitacion.Api.SetOfRules.Interface
{
    public interface IFriendRule
    {
        List<ResponseFriendJson> GetFriends();
        ResponseCodeToAddFriendJson GetCodeToAddFriend(string userToken);
        ResponseCodeWasReadJson CodeWasRead(string userToken, string code);
        void ApproveFriend(string adminId, string friendId, RequestApproveAddFriendJson requestApprove);
    }
}
