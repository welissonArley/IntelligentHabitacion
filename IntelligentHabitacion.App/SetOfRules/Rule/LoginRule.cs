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

        public async Task ChangePasswordForgotPassword(string email, string code, string newPassword, string confirmationPassword)
        {
            ValidateEmail(email);

            if (string.IsNullOrWhiteSpace(code))
                throw new CodeEmptyException();

            ValidatePasswordAndPasswordConfirmation(newPassword, confirmationPassword);

            await _httpClient.ChangePasswordForgotPassword(new RequestResetYourPasswordJson
            {
                Email = email,
                Code = code,
                Password = newPassword,
                PasswordConfirmation = confirmationPassword
            }, System.Globalization.CultureInfo.CurrentCulture.ToString());
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

        public async Task RequestCode(string email)
        {
            ValidateEmail(email);
            await _httpClient.RequestCodeResetPassword(email, System.Globalization.CultureInfo.CurrentCulture.ToString());
        }

        private void ValidateEmail(string email)
        {
            new EmailValidator().IsValid(email);
        }

        private void ValidatePasswordAndPasswordConfirmation(string newPassword, string confirmationPassword)
        {
            new PasswordValidator().IsValidaPasswordAndConfirmation(newPassword, confirmationPassword);
        }
    }
}
