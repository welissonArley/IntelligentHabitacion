using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.Communication;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Validators.Validator;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Rule
{
    public class LoginRule : ILoginRule
    {
        private readonly IIntelligentHabitacionHttpClient _httpClient;

        public LoginRule(IIntelligentHabitacionHttpClient intelligentHabitacionHttpClient)
        {
            _httpClient = intelligentHabitacionHttpClient;
        }

        public void ChangePasswordForgetPassword(string email, string code, string newPassword, string confirmationPassword)
        {
            ValidateEmail(email);

            if (string.IsNullOrWhiteSpace(code))
                throw new CodeEmptyException();

            ValidatePasswordAndPasswordConfirmation(newPassword, confirmationPassword);
        }

        public async Task<ResponseJson> Login(string email, string password)
        {
            ValidateEmail(email);

            if (string.IsNullOrWhiteSpace(password))
                throw new PasswordEmptyException();

            var response = await _httpClient.Login(new RequestLoginJson
            {
                User = email,
                Password = password
            }, System.Globalization.CultureInfo.CurrentCulture.ToString());

            return response;
        }

        public void RequestCode(string email)
        {
            ValidateEmail(email);
        }

        private void ValidateEmail(string email)
        {
            new EmailValidator().IsValid(email);
        }

        private void ValidatePasswordAndPasswordConfirmation(string newPassword, string confirmationPassword)
        {
            new PasswordValidator().IsValidaPasswordAndConfirmation(newPassword, confirmationPassword);
        }

        public void ChangePassword(string currentPassword, string newPassword, string confirmationPassword)
        {
            if (string.IsNullOrWhiteSpace(currentPassword))
                throw new CurrentPasswordEmptyException();

            ValidatePasswordAndPasswordConfirmation(newPassword, confirmationPassword);
        }
    }
}
