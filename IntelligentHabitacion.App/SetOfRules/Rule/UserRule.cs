using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.OneSignalConfig;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.Useful.Validator;
using IntelligentHabitacion.Communication;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Rule
{
    public class UserRule : IUserRule
    {
        private readonly IIntelligentHabitacionHttpClient _httpClient;
        private readonly UserPreferences _userPreferences;

        public UserRule(IIntelligentHabitacionHttpClient intelligentHabitacionHttpClient, UserPreferences userPreferences)
        {
            _httpClient = intelligentHabitacionHttpClient;
            _userPreferences = userPreferences;
        }

        public async Task ValidateEmailAndVerifyIfAlreadyBeenRegistered(string email)
        {
            ValidateEmail(email);
            var response = await _httpClient.EmailAlreadyBeenRegistered(email, System.Globalization.CultureInfo.CurrentCulture.ToString());
            if (response.Value)
                throw new EmailAlreadyBeenRegisteredException();
        }

        public void ValidateEmail(string email)
        {
            new EmailValidator().IsValid(email);
        }

        public void ValidateEmergencyContact(string name, string phoneNumber, string relationship)
        {
            ValidateName(name);
            if (string.IsNullOrWhiteSpace(relationship))
                throw new RelationshipToEmptyException();

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new PhoneNumberEmptyException();
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new NameEmptyException();
        }

        public void ValidatePassword(string password)
        {
            new PasswordValidator().IsValid(password);
        }

        public void DeleteAccount(string codeReceived, string password)
        {
            if (string.IsNullOrWhiteSpace(codeReceived))
                throw new CodeEmptyException();

            if (string.IsNullOrWhiteSpace(password))
                throw new PasswordEmptyException();
        }

        public async Task UpdateInformations(UserInformationsModel userInformations)
        {
            ValidateName(userInformations.Name);
            ValidateEmail(userInformations.Email);

            if (string.IsNullOrWhiteSpace(userInformations.PhoneNumber1))
                throw new PhoneNumberEmptyException();

            ValidateEmergencyContact(userInformations.EmergencyContact1.Name, userInformations.EmergencyContact1.PhoneNumber, userInformations.EmergencyContact1.Relationship);
            if(!string.IsNullOrWhiteSpace(userInformations.EmergencyContact2.Name))
                ValidateEmergencyContact(userInformations.EmergencyContact2.Name, userInformations.EmergencyContact2.PhoneNumber, userInformations.EmergencyContact2.Relationship);

            var updateUser = new RequestUpdateUserJson
            {
                Name = userInformations.Name,
                Email = userInformations.Email
            };
            updateUser.Phonenumbers.Add(userInformations.PhoneNumber1);
            if (!string.IsNullOrWhiteSpace(userInformations.PhoneNumber2))
                updateUser.Phonenumbers.Add(userInformations.PhoneNumber2);

            updateUser.EmergencyContacts.Add(new RequestEmergencyContactJson
            {
                Name = userInformations.EmergencyContact1.Name,
                Relationship = userInformations.EmergencyContact1.Relationship,
                Phonenumber = userInformations.EmergencyContact1.PhoneNumber
            });

            if (!string.IsNullOrWhiteSpace(userInformations.EmergencyContact2.Name))
            {
                updateUser.EmergencyContacts.Add(new RequestEmergencyContactJson
                {
                    Name = userInformations.EmergencyContact2.Name,
                    Relationship = userInformations.EmergencyContact2.Relationship,
                    Phonenumber = userInformations.EmergencyContact2.PhoneNumber
                });
            }

            var response = await _httpClient.UpdateUsersInformations(updateUser, _userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);
        }

        public async Task<ResponseJson> Create(RegisterUserModel userInformations)
        {
            ValidateName(userInformations.Name);
            ValidatePassword(userInformations.Password);
            ValidateEmergencyContact(userInformations.EmergencyContact1.Name, userInformations.EmergencyContact1.PhoneNumber, userInformations.EmergencyContact1.Relationship);
            if (!string.IsNullOrWhiteSpace(userInformations.EmergencyContact2.Name))
                ValidateEmergencyContact(userInformations.EmergencyContact2.Name, userInformations.EmergencyContact2.PhoneNumber, userInformations.EmergencyContact2.Relationship);

            var user = new RequestRegisterUserJson
            {
                Name = userInformations.Name,
                Email = userInformations.Email,
                Password = userInformations.Password,
                PushNotificationId = OneSignalManager.MyOneSignalId
            };
            user.Phonenumbers.Add(userInformations.PhoneNumber1);
            if(!string.IsNullOrWhiteSpace(userInformations.PhoneNumber2))
                user.Phonenumbers.Add(userInformations.PhoneNumber2);
            
            user.EmergencyContacts.Add(new RequestEmergencyContactJson
            {
                Name = userInformations.EmergencyContact1.Name,
                Relationship = userInformations.EmergencyContact1.Relationship,
                Phonenumber = userInformations.EmergencyContact1.PhoneNumber
            });
            if (!string.IsNullOrWhiteSpace(userInformations.EmergencyContact2.Name))
            {
                user.EmergencyContacts.Add(new RequestEmergencyContactJson
                {
                    Name = userInformations.EmergencyContact2.Name,
                    Relationship = userInformations.EmergencyContact2.Relationship,
                    Phonenumber = userInformations.EmergencyContact2.PhoneNumber
                });
            }

            return await _httpClient.CreateUser(user, System.Globalization.CultureInfo.CurrentCulture.ToString());
        }

        public async Task<UserInformationsModel> GetInformations()
        {
            var response = await _httpClient.GetUsersInformations(_userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);

            var userInformations = (ResponseUserInformationsJson)response.Response;

            var userInformationsModel = new UserInformationsModel
            {
                Name = userInformations.Name,
                Email = userInformations.Email,
                PhoneNumber1 = userInformations.Phonenumbers.First().Number
            };

            if (userInformations.Phonenumbers.Count > 1)
                userInformationsModel.PhoneNumber2 = userInformations.Phonenumbers[1].Number;

            var emergencyContact = userInformations.EmergencyContacts.First();
            userInformationsModel.EmergencyContact1 = new EmergencyContactModel
            {
                Name = emergencyContact.Name,
                Relationship = emergencyContact.Relationship,
                PhoneNumber = emergencyContact.Phonenumber
            };

            if(userInformations.EmergencyContacts.Count > 1)
            {
                emergencyContact = userInformations.EmergencyContacts[1];
                userInformationsModel.EmergencyContact2 = new EmergencyContactModel
                {
                    Name = emergencyContact.Name,
                    Relationship = emergencyContact.Relationship,
                    PhoneNumber = emergencyContact.Phonenumber
                };
            }

            return userInformationsModel;
        }

        public async Task ChangePassword(string currentPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(currentPassword))
                throw new CurrentPasswordEmptyException();

            ValidatePassword(newPassword);

            var response = await _httpClient.ChangePassword(new RequestChangePasswordJson
            {
                CurrentPassword = currentPassword,
                NewPassword = newPassword,
            },_userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);
        }
    }
}
