using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.View;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

namespace IntelligentHabitacion.App.ViewModel.RegisterUser
{
    public class RequestPasswordViewModel : BaseViewModel
    {
        private readonly IUserRule _userRule;
        private readonly UserPreferences _userPreferences;

        public ICommand OnConcludeCommand { protected set; get; }

        public RegisterUserModel Model { get; set; }

        public RequestPasswordViewModel(IUserRule userRule, UserPreferences userPreferences)
        {
            _userRule = userRule;
            _userPreferences = userPreferences;
            OnConcludeCommand = new Command(async () => await OnConclude());
        }

        private async Task OnConclude()
        {
            try
            {
                await ShowLoading();

                _userRule.ValidatePassword(Model.Password, Model.PasswordConfirmation);

                var response = await _userRule.Create(Model);

                _userPreferences.Name = Model.Name;
                _userPreferences.IsAdministrator = false;
                _userPreferences.IsPartOfOneHome = false;
                _userPreferences.Width = Application.Current.MainPage.Width;
                _userPreferences.Token = response.Token;
                _userPreferences.ProfileColor = response.Response.ToString();

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
