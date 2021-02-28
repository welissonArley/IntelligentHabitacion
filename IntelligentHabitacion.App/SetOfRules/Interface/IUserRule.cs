using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.Communication.Response;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Interface
{
    public interface IUserRule
    {
        void ValidateName(string name);
        Task ValidateEmailAndVerifyIfAlreadyBeenRegistered(string email);
        void ValidateEmail(string email);
        void ValidateEmergencyContact(string name, string phoneNumber, string relationship);
        void ValidatePassword(string password);
        void DeleteAccount(string codeReceived, string password);
        Task<ResponseJson> Create(RegisterUserModel userInformations);
        Task UpdateInformations(UserInformationsModel userInformations);
        Task<UserInformationsModel> GetInformations();
        Task ChangePassword(string currentPassword, string newPassword);
    }
}
