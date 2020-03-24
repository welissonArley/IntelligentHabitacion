using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.SQLite.Interface;
using IntelligentHabitacion.Communication;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Validators.Validator;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Rule
{
    public class UserRule : IUserRule
    {
        private readonly IIntelligentHabitacionHttpClient _httpClient;
        private readonly ISqliteDatabase _database;

        public UserRule(IIntelligentHabitacionHttpClient intelligentHabitacionHttpClient, ISqliteDatabase database)
        {
            _httpClient = intelligentHabitacionHttpClient;
            _database = database;
        }

        public async Task ValidateEmailAndVerifyIfAlreadyBeenRegistered(string email)
        {
            ValidateEmail(email);
            var response = await _httpClient.EmailAlreadyBeenRegistered(email);
            if (response.Value)
                throw new EmailAlreadyBeenRegisteredException();
        }

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
            ValidateEmergencyContact(userInformations.EmergencyContact1.Name, userInformations.EmergencyContact1.PhoneNumber, userInformations.EmergencyContact1.FamilyRelationship);
            if(!string.IsNullOrWhiteSpace(userInformations.EmergencyContact2.Name))
                ValidateEmergencyContact(userInformations.EmergencyContact2.Name, userInformations.EmergencyContact2.PhoneNumber, userInformations.EmergencyContact2.FamilyRelationship);

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
                DegreeOfKinship = userInformations.EmergencyContact1.FamilyRelationship,
                Phonenumbers = new List<string> { userInformations.EmergencyContact1.PhoneNumber }
            });

            if (!string.IsNullOrWhiteSpace(userInformations.EmergencyContact2.Name))
            {
                updateUser.EmergencyContacts.Add(new RequestEmergencyContactJson
                {
                    Name = userInformations.EmergencyContact2.Name,
                    DegreeOfKinship = userInformations.EmergencyContact2.FamilyRelationship,
                    Phonenumbers = new List<string> { userInformations.EmergencyContact2.PhoneNumber }
                });
            }

            var response = await _httpClient.UpdateUsersInformations(updateUser, _database.Get().Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _database.UpdateName(userInformations.Name);
            _database.UpdateToken(response.Token);
        }

        public async Task<ResponseJson> Create(RegisterUserModel userInformations)
        {
            ValidateName(userInformations.Name);
            await ValidateEmailAndVerifyIfAlreadyBeenRegistered(userInformations.Email);
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

            return await _httpClient.CreateUser(user, System.Globalization.CultureInfo.CurrentCulture.ToString());
        }

        public UserInformationsModel GetInformations()
        {
            var response = Task.Run(async () => await _httpClient.GetUsersInformations(_database.Get().Token, System.Globalization.CultureInfo.CurrentCulture.ToString())).Result;

            _database.UpdateToken(response.Token);

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
                FamilyRelationship = emergencyContact.DegreeOfKinship,
                PhoneNumber = emergencyContact.Phonenumbers.First().Number
            };

            if(userInformations.EmergencyContactc.Count > 1)
            {
                emergencyContact = userInformations.EmergencyContactc[1];
                userInformationsModel.EmergencyContact2 = new EmergencyContactModel
                {
                    Name = emergencyContact.Name,
                    FamilyRelationship = emergencyContact.DegreeOfKinship,
                    PhoneNumber = emergencyContact.Phonenumbers.First().Number
                };
            }

            return userInformationsModel;
        }
    }
}
