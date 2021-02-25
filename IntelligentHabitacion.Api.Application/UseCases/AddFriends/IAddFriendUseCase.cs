using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.AddFriends
{
    public interface IAddFriendUseCase
    {
        Task<ResponseCodeToAddFriendJson> GetCodeToAddFriend(string userToken);
        Task<ResponseCodeWasReadJson> CodeWasRead(string userToken, string code);
        Task ApproveFriend(string adminId, string friendId, RequestApproveAddFriendJson requestApprove);
    }
}
