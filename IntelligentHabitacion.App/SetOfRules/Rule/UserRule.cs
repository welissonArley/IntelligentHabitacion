using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.Communication;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Validators.Validator;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Rule
{
    public class UserRule : IUserRule
    {
        private readonly IntelligentHabitacionHttpClient _httpClient;

        public UserRule(IntelligentHabitacionHttpClient intelligentHabitacionHttpClient)
        {
            _httpClient = intelligentHabitacionHttpClient;
        }

        public async Task ValidateEmail(string email)
        {
            new EmailValidator().IsValid(email);
            var response = await _httpClient.EmailAlreadyBeenRegistered(email);
            if (response.Value)
                throw new EmailAlreadyBeenRegisteredException();
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

        public void DeleteAccount(string codeReceived, string password)
        {
            if (string.IsNullOrWhiteSpace(codeReceived))
                throw new CodeEmptyException();

            if (string.IsNullOrWhiteSpace(password))
                throw new PasswordEmptyException();
        }

        public async void UpdateInformations(UserInformationsModel userInformations)
        {
            ValidateName(userInformations.Name);
            await ValidateEmail(userInformations.Email);
            ValidatePhoneNumber(userInformations.PhoneNumber1, userInformations.PhoneNumber2);
            ValidateEmergencyContact(userInformations.EmergencyContact1.Name, userInformations.EmergencyContact1.PhoneNumber, userInformations.EmergencyContact1.FamilyRelationship);
            if(!string.IsNullOrWhiteSpace(userInformations.EmergencyContact2.Name))
                ValidateEmergencyContact(userInformations.EmergencyContact2.Name, userInformations.EmergencyContact2.PhoneNumber, userInformations.EmergencyContact2.FamilyRelationship);
        }

        public async Task Create(RegisterUserModel userInformations)
        {
            ValidateName(userInformations.Name);
            await ValidateEmail(userInformations.Email);
            ValidatePassword(userInformations.Password, userInformations.PasswordConfirmation);
            ValidatePhoneNumber(userInformations.PhoneNumber1, userInformations.PhoneNumber2);
            ValidateEmergencyContact(userInformations.EmergencyContact1.Name, userInformations.EmergencyContact1.PhoneNumber, userInformations.EmergencyContact1.FamilyRelationship);
            if (!string.IsNullOrWhiteSpace(userInformations.EmergencyContact2.Name))
                ValidateEmergencyContact(userInformations.EmergencyContact2.Name, userInformations.EmergencyContact2.PhoneNumber, userInformations.EmergencyContact2.FamilyRelationship);

            var user = new RequestRegisterUserJson
            {
                Name = userInformations.Name,
                Email = userInformations.Email,
                Password = userInformations.Password,
                PasswordConfirmation = userInformations.PasswordConfirmation
            };
            user.Phonenumbers.Add(userInformations.PhoneNumber1);
            if(!string.IsNullOrWhiteSpace(userInformations.PhoneNumber2))
                user.Phonenumbers.Add(userInformations.PhoneNumber2);
            
            user.EmergencyContacts.Add(new RequestEmergencyContactJson
            {
                Name = userInformations.EmergencyContact1.Name,
                DegreeOfKinship = userInformations.EmergencyContact1.FamilyRelationship,
                Phonenumbers = new List<string> { userInformations.EmergencyContact1.PhoneNumber }
            });
            if (!string.IsNullOrWhiteSpace(userInformations.EmergencyContact2.Name))
            {
                user.EmergencyContacts.Add(new RequestEmergencyContactJson
                {
                    Name = userInformations.EmergencyContact2.Name,
                    DegreeOfKinship = userInformations.EmergencyContact2.FamilyRelationship,
                    Phonenumbers = new List<string> { userInformations.EmergencyContact2.PhoneNumber }
                });
            }

            await _httpClient.CreateUser(user, System.Globalization.CultureInfo.CurrentCulture.ToString());
        }
    }
}
