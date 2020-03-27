using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.SQLite.Interface;
using IntelligentHabitacion.App.View;
using IntelligentHabitacion.App.ViewModel.RegisterUser;
using IntelligentHabitacion.Communication.Response;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

namespace IntelligentHabitacion.App.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ILoginRule _loginRule;
        private readonly ISqliteDatabase _database;

        public ICommand LoginCommand { protected set; get; }
        public ICommand RegisterCommand { protected set; get; }
        public ICommand ForgotPasswordCommand { protected set; get; }

        public string Email { get; set; }
        public string Password { get; set; }
        
        public LoginViewModel(ILoginRule loginRule, ISqliteDatabase database)
        {
            _database = database;
            _loginRule = loginRule;
            LoginCommand = new Command(async () => await OnLogin());
            RegisterCommand = new Command(async () => await OnRegister());
            ForgotPasswordCommand = new Command(async () => await OnForgotPassword());
        }

        private async Task OnLogin()
        {
            try
            {
                await ShowLoading();

                var response = await _loginRule.Login(Email, Password);

                var responseLogin = (ResponseLoginJson)response.Response;

                _database.Save(new SQLite.Model.UserSqlite
                {
                    Name = responseLogin.Name,
                    IsAdministrator = responseLogin.IsAdministrator,
                    IsPartOfOneHome = responseLogin.IsPartOfOneHome,
                    Width = Application.Current.MainPage.Width,
                    Token = response.Token
                });

                Application.Current.MainPage = new NavigationPage((Page)ViewFactory.CreatePage<UserWithoutPartOfHomeViewModel, UserWithoutPartOfHomePage>());

                HideLoading();
            }
            catch(System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
        private async Task OnRegister()
        {
            try
            {
                await Navigation.PushAsync<RequestEmailViewModel>((viewModel, page) => viewModel.Model = new Model.RegisterUserModel());
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private async Task OnForgotPassword()
        {
            try
            {
                await Navigation.PushAsync<ForgotPassword.RequestEmailViewModel>((viewModel, page) => viewModel.Model = new Model.ForgetPasswordModel());
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
