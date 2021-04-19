using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services.Communication.Login;
using IntelligentHabitacion.App.Useful.Validator;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using Refit;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.Login.ForgotPassword
{
    public class ResetPasswordUseCase : UseCaseBase, IResetPasswordUseCase
    {
        private readonly ILoginRestService _restService;

        public ResetPasswordUseCase() : base("Login")
        {
            _restService = RestService.For<ILoginRestService>(BaseAddress());
        }

        public async Task Execute(ForgetPasswordModel model)
        {
            ValidateEmail(model.Email);

            if (string.IsNullOrWhiteSpace(model.CodeReceived))
                throw new CodeEmptyException();

            ValidatePassword(model.NewPassword);

            await _restService.ChangePasswordForgotPassword(new RequestResetYourPasswordJson
            {
                Email = model.Email,
                Code = model.CodeReceived,
                Password = model.NewPassword,
            }, GetLanguage());
        }

        private void ValidateEmail(string email)
        {
            new EmailValidator().IsValid(email);
        }

        private void ValidatePassword(string newPassword)
        {
            new PasswordValidator().IsValid(newPassword);
        }
    }
}
