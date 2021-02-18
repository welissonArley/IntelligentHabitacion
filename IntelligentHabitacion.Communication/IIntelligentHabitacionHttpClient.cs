using IntelligentHabitacion.Communication.Boolean;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Communication
{
    public interface IIntelligentHabitacionHttpClient
    {
        Task<BooleanJson> EmailAlreadyBeenRegistered(string email, string language = null);
        Task<ResponseJson> CreateUser(RequestRegisterUserJson registerUser, string language = null);
        Task<ResponseLocationBrazilJson> GetLocationBrazilByZipCode(string zipcode);
        Task<ResponseJson> Login(RequestLoginJson loginUser, string language = null);
        Task RequestCodeResetPassword(string email, string language = null);
        Task ChangePasswordForgotPassword(RequestResetYourPasswordJson resetYourPassword, string language = null);
        Task<ResponseJson> GetUsersInformations(string token, string language = null);
        Task<ResponseJson> UpdateUsersInformations(RequestUpdateUserJson updateUser, string token, string language = null);
        Task<ResponseJson> ChangePassword(RequestChangePasswordJson changePassword, string token, string language = null);
        Task<ResponseJson> CreateHome(RequestRegisterHomeJson registerHome, string token, string language = null);
        Task<ResponseJson> UpdateHome(RequestRegisterHomeJson registerHome, string token, string language = null);
        Task<ResponseJson> DeleteHome(RequestAdminActionJson request, string token, string language = null);
        Task<ResponseJson> GetHomesInformations(string token, string language = null);
        Task<ResponseJson> GetHouseFriends(string token, string language = null);
        Task<ResponseJson> AddMyFood(RequestAddMyFoodJson myFood, string token, string language = null);
        Task<ResponseJson> EditMyFood(RequestEditMyFoodJson myFood, string token, string language = null);
        Task<ResponseJson> DeleteMyFood(string id, string token, string language = null);
        Task<ResponseJson> ChangeQuantityMyFood(RequestChangeQuantityMyFoodJson myFood, string token, string language = null);
        Task<ResponseJson> GetMyFoods(string token, string language = null);
        Task<ResponseJson> ChangeDateJoinHome(RequestChangeDateJoinHomeJson request, string token, string language = null);
        Task<ResponseJson> NotifyFriendOrderHasArrived(string friendId, string token, string language = null);
        Task<ResponseJson> RequestCodeToChangeAdministrator(string token, string language = null);
        Task<ResponseJson> RequestCodeToRemoveFriend(string token, string language = null);
        Task<ResponseJson> RequestCodeToDeleteHome(string token, string language = null);
        Task<ResponseJson> ChangeAdministrator(RequestAdminActionsOnFriendJson request, string token, string language = null);
        Task<ResponseJson> RemoveFriend(RequestAdminActionsOnFriendJson request, string token, string language = null);
    }
}
