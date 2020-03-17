using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.WorkUnit;
using IntelligentHabitacion.Api.SetOfRules.Cryptography;
using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Api.Validators;
using IntelligentHabitacion.Communication.Boolean;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using IntelligentHabitacion.Validators.Validator;
using System.Linq;

namespace IntelligentHabitacion.Api.SetOfRules.Rule
{
    public class UserRule : IUserRule
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptographyPassword _cryptography;

        public UserRule(IUserRepository userRepository, ICryptographyPassword cryptography)
        {
            _userRepository = userRepository;
            _cryptography = cryptography;
        }

        public BooleanJson EmailAlreadyBeenRegistered(string email)
        {
            new EmailValidator().IsValid(email);

            var result = new BooleanJson
            {
                Value = _userRepository.GetAllActive().AsEnumerable().Any(c => c.Email.Equals(email))
            };

            return result;
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
            {
                _userRepository.Create(userModel);
                /*
                 * Please, the next line of code is only needed here ok?
                 * Because the token will be generated and the user must actually have the commit already made.
                 */
                WorkUnitNHibernate.WorkUnitNHibernateActive.Commit();
            }
            else
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
