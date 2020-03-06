using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.SetOfRules.Cryptography;
using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Api.Validators;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception.ExceptionsBase;
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

        public void Register(RequestRegisterUserJson registerUserJson)
        {
            var userModel = new Mapper.Mapper().MapperJsonToModel(registerUserJson);
            userModel.Password = _cryptography.Encrypt(userModel.Password);

            var validation = new UserValidator().Validate(userModel);

            if (validation.IsValid)
                _userRepository.Create(userModel);
            else
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
