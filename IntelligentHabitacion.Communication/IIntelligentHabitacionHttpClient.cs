using IntelligentHabitacion.Communication.Boolean;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using System.Collections.Generic;
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
        Task<ResponseJson> UpdateHome(RequestUpdateHomeJson registerHome, string token, string language = null);
        Task<ResponseJson> DeleteHome(RequestAdminActionJson request, string token, string language = null);
        Task<ResponseJson> GetHomesInformations(string token, string language = null);
        Task<ResponseJson> GetHouseFriends(string token, string language = null);
        Task<ResponseJson> AddMyFood(RequestProductJson myFood, string token, string language = null);
        Task<ResponseJson> EditMyFood(string myFoodId, RequestProductJson myFood, string token, string language = null);
        Task<ResponseJson> DeleteMyFood(string id, string token, string language = null);
        Task<ResponseJson> ChangeQuantityMyFood(string myFoodId, decimal amount, string token, string language = null);
        Task<ResponseJson> GetMyFoods(string token, string language = null);
        Task<ResponseJson> ChangeDateJoinHome(string friendId, RequestDateJson request, string token, string language = null);
        Task<ResponseJson> NotifyFriendOrderHasArrived(string friendId, string token, string language = null);
        Task<ResponseJson> RequestCodeToChangeAdministrator(string token, string language = null);
        Task<ResponseJson> RequestCodeToRemoveFriend(string token, string language = null);
        Task<ResponseJson> RequestCodeToDeleteHome(string token, string language = null);
        Task<ResponseJson> ChangeAdministrator(string friendId, RequestAdminActionJson request, string token, string language = null);
        Task<ResponseJson> RemoveFriend(string friendId, RequestAdminActionJson request, string token, string language = null);
        Task<ResponseJson> GetMyTasksCleaningSchedule(string token, RequestDateJson date, string language = null);
        Task<ResponseJson> GetCleaningSchedule(string token, string language = null);
        Task<ResponseJson> UpdateCleaningSchedule(string token, IList<RequestUpdateCleaningScheduleJson> request, string language = null);
        Task<ResponseJson> TaskCompletedToday(string token, string id, string language = null);
        Task<ResponseJson> GetUsersTaskDetails(string token, string id, RequestDateJson dateJson, string language = null);
    }
}
