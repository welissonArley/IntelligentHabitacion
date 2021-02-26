using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.Useful.Validator;
using IntelligentHabitacion.Communication;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
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

        public async Task ChangePasswordForgotPassword(ForgetPasswordModel model)
        {
            ValidateEmail(model.Email);

            if (string.IsNullOrWhiteSpace(model.CodeReceived))
                throw new CodeEmptyException();

            ValidatePasswordAndPasswordConfirmation(model.NewPassword);

            await _httpClient.ChangePasswordForgotPassword(new RequestResetYourPasswordJson
            {
                Email = model.Email,
                Code = model.CodeReceived,
                Password = model.NewPassword,
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

        private void ValidatePasswordAndPasswordConfirmation(string newPassword)
        {
            new PasswordValidator().IsValidaPasswordAndConfirmation(newPassword);
        }
    }
}
