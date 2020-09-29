﻿using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.OneSignalConfig;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.Communication;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Validators.Validator;
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
            ValidatePhoneNumber(phoneNumber, null);
            if (string.IsNullOrWhiteSpace(relationship))
                throw new RelationshipToEmptyException();
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

        public async Task UpdateInformations(UserInformationsModel userInformations)
        {
            ValidateName(userInformations.Name);
            ValidateEmail(userInformations.Email);
            ValidatePhoneNumber(userInformations.PhoneNumber1, userInformations.PhoneNumber2);
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
            await ValidateEmailAndVerifyIfAlreadyBeenRegistered(userInformations.Email);
            ValidatePassword(userInformations.Password, userInformations.PasswordConfirmation);
            ValidatePhoneNumber(userInformations.PhoneNumber1, userInformations.PhoneNumber2);
            ValidateEmergencyContact(userInformations.EmergencyContact1.Name, userInformations.EmergencyContact1.PhoneNumber, userInformations.EmergencyContact1.Relationship);
            if (!string.IsNullOrWhiteSpace(userInformations.EmergencyContact2.Name))
                ValidateEmergencyContact(userInformations.EmergencyContact2.Name, userInformations.EmergencyContact2.PhoneNumber, userInformations.EmergencyContact2.Relationship);

            var user = new RequestRegisterUserJson
            {
                Name = userInformations.Name,
                Email = userInformations.Email,
                Password = userInformations.Password,
                PasswordConfirmation = userInformations.PasswordConfirmation,
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

            var emergencyContact = userInformations.EmergencyContactc.First();
            userInformationsModel.EmergencyContact1 = new EmergencyContactModel
            {
                Name = emergencyContact.Name,
                Relationship = emergencyContact.Relationship,
                PhoneNumber = emergencyContact.Phonenumber
            };

            if(userInformations.EmergencyContactc.Count > 1)
            {
                emergencyContact = userInformations.EmergencyContactc[1];
                userInformationsModel.EmergencyContact2 = new EmergencyContactModel
                {
                    Name = emergencyContact.Name,
                    Relationship = emergencyContact.Relationship,
                    PhoneNumber = emergencyContact.Phonenumber
                };
            }

            return userInformationsModel;
        }

        public async Task ChangePassword(string currentPassword, string newPassword, string confirmationPassword)
        {
            if (string.IsNullOrWhiteSpace(currentPassword))
                throw new CurrentPasswordEmptyException();

            ValidatePassword(newPassword, confirmationPassword);

            var response = await _httpClient.ChangePassword(new RequestChangePasswordJson
            {
                CurrentPassword = currentPassword,
                NewPassword = newPassword,
                NewPasswordConfirmation = confirmationPassword
            },_userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);
        }
    }
}
