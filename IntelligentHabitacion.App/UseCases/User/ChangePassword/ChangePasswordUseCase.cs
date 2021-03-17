using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.Services.Communication.User;
using IntelligentHabitacion.App.Useful.Validator;
using IntelligentHabitacion.Exception;
using Refit;
using System;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.User.ChangePassword
{
    public class ChangePasswordUseCase : UseCaseBase, IChangePasswordUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly IUserRestService _restService;

        public ChangePasswordUseCase(Lazy<UserPreferences> userPreferences) : base("User")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<IUserRestService>(BaseAddress());
        }

        public async Task Execute(string currentPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(currentPassword))
                throw new CurrentPasswordEmptyException();

            ValidatePassword(newPassword);

            var response = await _restService.ChangePassword(new Communication.Request.RequestChangePasswordJson
            {
                CurrentPassword = currentPassword,
                NewPassword = newPassword
            }, await _userPreferences.GetToken(), GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));
            await _userPreferences.ChangePassword(newPassword);
        }

        public void ValidatePassword(string password)
        {
            new PasswordValidator().IsValid(password);
        }
    }
}
