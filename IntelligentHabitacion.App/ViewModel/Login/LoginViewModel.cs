using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.View.Login;
using IntelligentHabitacion.Communication.Response;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

namespace IntelligentHabitacion.App.ViewModel.Login
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ILoginRule _loginRule;

        public ICommand ButtonLoginCommand { protected set; get; }
        public ICommand ForgotPasswordCommand { protected set; get; }
        public ICommand UsingFigerprintToLoginCommand { protected set; get; }
        private readonly UserPreferences _userPreferences;

        public string Email { get; set; }
        public string Password { get; set; }
        public bool CanUseFigerprintToLogin { get; set; }

        public LoginViewModel(ILoginRule loginRule, UserPreferences userPreferences)
        {
            _loginRule = loginRule;
            _userPreferences = userPreferences;
            CanUseFigerprintToLogin = userPreferences.AlreadySignedIn && CrossFingerprint.Current.IsAvailableAsync().GetAwaiter().GetResult();

            ForgotPasswordCommand = new Command(async () => await OnForgotPassword());
            ButtonLoginCommand = new Command(async () =>
            {
                await DoLogin(Email, Password);
            });
            
            if (CanUseFigerprintToLogin)
            {
                UsingFigerprintToLoginCommand = new Command(async () =>
                {
                    var request = new AuthenticationRequestConfiguration(ResourceText.TITLE_LOGIN_WITH_FINGERPRINT_ACCESS, "");
                    var result = await CrossFingerprint.Current.AuthenticateAsync(request);

                    if (result.Authenticated)
                        await DoLogin(userPreferences.Email, userPreferences.Password);
                });

                UsingFigerprintToLoginCommand.Execute(null);
            }
        }

        private async Task OnForgotPassword()
        {
            try
            {
                await Navigation.PushAsync<User.ForgotPassword.RequestEmailViewModel>((viewModel, page) => viewModel.Model = new Model.ForgetPasswordModel());
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        private async Task DoLogin(string email, string password)
        {
            try
            {
                await ShowLoading();

                var response = await _loginRule.Login(email, password);

                var responseLogin = (ResponseLoginJson)response.Response;

                _userPreferences.SaveUserInformations(new Dtos.UserPreferenceDto
                {
                    Email = email, Token = response.Token, Password = password, Name = responseLogin.Name,
                    IsAdministrator = responseLogin.IsAdministrator, ProfileColor = responseLogin.ProfileColor,
                    IsPartOfOneHome = responseLogin.IsPartOfOneHome, Width = Application.Current.MainPage.Width,
                    Id = responseLogin.Id
                });

                if (responseLogin.IsPartOfOneHome)
                    Application.Current.MainPage = new NavigationPage((Page)ViewFactory.CreatePage<UserIsPartOfHomeViewModel, UserIsPartOfHomePage>());
                else
                    Application.Current.MainPage = new NavigationPage((Page)ViewFactory.CreatePage<UserWithoutPartOfHomeViewModel, UserWithoutPartOfHomePage>());

                HideLoading();

                await Navigation.PopToRootAsync();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
    }
}
