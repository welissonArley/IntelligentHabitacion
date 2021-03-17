using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.Services.Communication.Login;
using IntelligentHabitacion.App.Useful.Validator;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using Refit;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.UseCases.Login.DoLogin
{
    public class LoginUseCase : UseCaseBase, ILoginUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly ILoginRestService _restService;

        public LoginUseCase(Lazy<UserPreferences> userPreferences) : base("Login")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<ILoginRestService>(BaseAddress());
        }

        public async Task<bool> Execute(string email, string password)
        {
            ValidateEmail(email);

            if (string.IsNullOrWhiteSpace(password))
                throw new PasswordEmptyException();

            var response = await _restService.DoLogin(new RequestLoginJson
            {
                Password = password,
                User = email
            }, GetLanguage());

            ResponseValidate(response);

            await _userPreferences.SaveInitialUserInfos(new Dtos.UserPreferenceDto
            {
                Email = email,
                Token = GetTokenOnHeaderRequest(response.Headers),
                Password = password,
                Name = response.Content.Name,
                IsAdministrator = response.Content.IsAdministrator,
                ProfileColor = response.Content.ProfileColor,
                IsPartOfOneHome = response.Content.IsPartOfOneHome,
                Width = Application.Current.MainPage.Width,
                Id = response.Content.Id
            });

            return response.Content.IsPartOfOneHome;
        }

        private void ValidateEmail(string email)
        {
            new EmailValidator().IsValid(email);
        }
    }
}
