using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Validators.Validator;

namespace IntelligentHabitacion.App.SetOfRules.Rule
{
    public class UserRule : IUserRule
    {
        public void ValidateEmail(string email)
        {
            new EmailValidator().IsValid(email);
        }

        public void ValidateEmergencyContact(string name, string phoneNumber, string degreeKinship)
        {
            ValidateName(name);
            ValidatePhoneNumber(phoneNumber, null);
            if (string.IsNullOrWhiteSpace(degreeKinship))
                throw new DegreeKinshipEmptyException();
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new NameEmptyException();
        }

        public void ValidatePassword(string password, string confirmationPassword)
        {
            new PasswordValidator().IsValidaPasswordAndConfirmation(password, confirmationPassword);
        }

        public void ValidatePhoneNumber(string phoneNumber1, string phoneNumber2)
        {
            var phoneNumberValidator = new PhoneNumberValidator();

            phoneNumberValidator.IsValid(phoneNumber1);

            if (!string.IsNullOrWhiteSpace(phoneNumber2))
                phoneNumberValidator.IsValid(phoneNumber2);
        }

        public void DeleteAccount(string code, string password)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new CodeEmptyException();

            if (string.IsNullOrWhiteSpace(password))
                throw new PasswordEmptyException();
        }

        public void UpdateInformations(UserInformationsModel userInformations)
        {
            ValidateName(userInformations.Name);
            ValidateEmail(userInformations.Email);
            ValidatePhoneNumber(userInformations.PhoneNumber1, userInformations.PhoneNumber2);
            ValidateEmergencyContact(userInformations.EmergencyContact1.Name, userInformations.EmergencyContact1.PhoneNumber, userInformations.EmergencyContact1.FamilyRelationship);
            if(!string.IsNullOrWhiteSpace(userInformations.EmergencyContact2.Name))
                ValidateEmergencyContact(userInformations.EmergencyContact2.Name, userInformations.EmergencyContact2.PhoneNumber, userInformations.EmergencyContact2.FamilyRelationship);
        }
    }
}
