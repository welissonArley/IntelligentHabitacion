using IntelligentHabitacion.App.Model;

namespace IntelligentHabitacion.App.SetOfRules.Interface
{
    public interface IUserRule
    {
        void ValidateName(string name);
        void ValidateEmail(string email);
        void ValidatePhoneNumber(string phoneNumber1, string phoneNumber2);
        void ValidateEmergencyContact(string name, string phoneNumber, string degreeKinship);
        void ValidatePassword(string password, string confirmationPassword);
        void DeleteAccount(string codeReceived, string password);
        void UpdateInformations(UserInformationsModel userInformations);
    }
}
