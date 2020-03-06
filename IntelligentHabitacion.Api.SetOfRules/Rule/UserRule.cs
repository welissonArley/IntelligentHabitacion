using IntelligentHabitacion.Api.Repository.Interface;
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

        public UserRule(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Register(RequestRegisterUserJson registerUserJson)
        {
            var userModel = new Mapper.Mapper().MapperJsonToModel(registerUserJson);

            var validation = new UserValidator().Validate(userModel);

            if (validation.IsValid)
                _userRepository.Create(userModel);
            else
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
