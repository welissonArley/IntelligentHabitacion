using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;

namespace IntelligentHabitacion.Api.Services.Interface
{
    public interface IAddFriendRule
    {
        ResponseCodeToAddFriendJson GetCodeToAddFriend(string userToken);
        ResponseCodeWasReadJson CodeWasRead(string userToken, string code);
        void ApproveFriend(string adminId, string friendId, RequestApproveAddFriendJson requestApprove);
    }
}
