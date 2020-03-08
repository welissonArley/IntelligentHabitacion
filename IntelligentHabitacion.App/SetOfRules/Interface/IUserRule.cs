using IntelligentHabitacion.App.Model;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Interface
{
    public interface IUserRule
    {
        void ValidateName(string name);
        Task ValidateEmail(string email);
        void ValidatePhoneNumber(string phoneNumber1, string phoneNumber2);
        void ValidateEmergencyContact(string name, string phoneNumber, string degreeKinship);
        void ValidatePassword(string password, string confirmationPassword);
        void DeleteAccount(string codeReceived, string password);
        Task Create(RegisterUserModel userInformations);
        void UpdateInformations(UserInformationsModel userInformations);
    }
}
