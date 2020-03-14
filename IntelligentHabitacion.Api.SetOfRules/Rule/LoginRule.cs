using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.SetOfRules.Cryptography;
using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Api.SetOfRules.Rule
{
    public class LoginRule : ILoginRule
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptographyPassword _cryptography;

        public LoginRule(IUserRepository userRepository, ICryptographyPassword cryptography)
        {
            _userRepository = userRepository;
            _cryptography = cryptography;
        }

        public ResponseLoginJson DoLogin(RequestLoginJson loginJson)
        {
            var user = _userRepository.GetUserByEmail(loginJson.User);

            if (user == null || !user.Password.Equals(_cryptography.Encrypt(loginJson.Password)))
                throw new InvalidLoginException();

            return new Mapper.Mapper().MapperModelToJsonLogin(user);
        }
    }
}
