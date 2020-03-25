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
        void ValidatePhoneNumber(string phoneNumber1, string phoneNumber2);
        void ValidateEmergencyContact(string name, string phoneNumber, string degreeKinship);
        void ValidatePassword(string password, string confirmationPassword);
        void DeleteAccount(string codeReceived, string password);
        Task<ResponseJson> Create(RegisterUserModel userInformations);
        Task UpdateInformations(UserInformationsModel userInformations);
        UserInformationsModel GetInformations();
        Task ChangePassword(string currentPassword, string newPassword, string confirmationPassword);
    }
}
