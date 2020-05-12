using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.SetOfRules.Cryptography;
using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Api.SetOfRules.LoggedUser;
using IntelligentHabitacion.Api.Validators;
using IntelligentHabitacion.Communication.Boolean;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.API;
using IntelligentHabitacion.Exception.ExceptionsBase;
using IntelligentHabitacion.Useful;
using IntelligentHabitacion.Validators.Validator;
using System.Linq;

namespace IntelligentHabitacion.Api.SetOfRules.Rule
{
    public class UserRule : IUserRule
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptographyPassword _cryptography;
        private readonly ILoggedUser _loggedUser;

        public UserRule(IUserRepository userRepository, ICryptographyPassword cryptography, ILoggedUser loggedUser)
        {
            _userRepository = userRepository;
            _cryptography = cryptography;
            _loggedUser = loggedUser;
        }

        public void ChangePassword(RequestChangePasswordJson changePasswordJson)
        {
            new PasswordValidator().IsValidaPasswordAndConfirmation(changePasswordJson.NewPassword, changePasswordJson.NewPasswordConfirmation);

            var loggedUser = _loggedUser.User();

            var userToUpdate = _userRepository.GetById(loggedUser.Id);

            if (!userToUpdate.Password.Equals(_cryptography.Encrypt(changePasswordJson.CurrentPassword)))
                throw new CurrentPasswordException();

            userToUpdate.Password = _cryptography.Encrypt(changePasswordJson.NewPassword);
            _userRepository.Update(userToUpdate);
        }

        public BooleanJson EmailAlreadyBeenRegistered(string email)
        {
            new EmailValidator().IsValid(email);

            var result = new BooleanJson
            {
                Value = _userRepository.EmailAlreadyBeenRegistered(email)
            };

            return result;
        }

        public ResponseUserInformationsJson GetInformations()
        {
            var loggedUser = _loggedUser.User();

            return new Mapper.Mapper().MapperModelToJson(loggedUser);
        }

        public void Register(RequestRegisterUserJson registerUserJson)
        {
            new PasswordValidator().IsValidaPasswordAndConfirmation(registerUserJson.Password, registerUserJson.PasswordConfirmation);
            if (EmailAlreadyBeenRegistered(registerUserJson.Email).Value)
                throw new EmailAlreadyBeenRegisteredException();

            var userModel = new Mapper.Mapper().MapperJsonToModel(registerUserJson);
            userModel.Password = _cryptography.Encrypt(userModel.Password);

            var validation = new UserValidator().Validate(userModel);

            if (validation.IsValid)
                _userRepository.Create(userModel);
            else
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }

        public void Update(RequestUpdateUserJson updateUserJson)
        {
            var loggedUser = _loggedUser.User();
            var userToUpdate = _userRepository.GetById(loggedUser.Id);

            userToUpdate.UpdateDate = DateTimeController.DateTimeNow();
            userToUpdate.Name = updateUserJson.Name;
            userToUpdate.Email = updateUserJson.Email;
            userToUpdate.Phonenumbers.Clear();
            userToUpdate.EmergecyContacts.Clear();

            var mapper = new Mapper.Mapper();

            userToUpdate.Phonenumbers = updateUserJson.Phonenumbers.Select(c => mapper.MapperJsonToModel(c)).ToList();
            userToUpdate.EmergecyContacts = updateUserJson.EmergencyContacts.Select(c => mapper.MapperJsonToModel(c)).ToList();

            var validation = new UserValidator().Validate(userToUpdate);

            if (validation.IsValid)
                _userRepository.Update(userToUpdate);
            else
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
