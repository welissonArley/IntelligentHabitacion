using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.DeleteAccount
{
    public class DeleteAccountViewModel : BaseViewModel
    {
        private readonly IUserRule _userRule;

        public string ConfirmationCode { get; set; }
        public string Password { get; set; }

        private readonly UserPreferences _userPreferences;

        public ICommand ConfirmCommand { protected set; get; }

        public DeleteAccountViewModel(IUserRule userRule, UserPreferences userPreferences)
        {
            _userRule = userRule;
            ConfirmCommand = new Command(async () => await OnConfirm());
            _userPreferences = userPreferences;
        }

        private async Task OnConfirm()
        {
            try
            {
                _userRule.DeleteAccount(ConfirmationCode, Password);
                _userPreferences.ClearAll();
                Application.Current.MainPage = new NavigationPage((Page)XLabs.Forms.Mvvm.ViewFactory.CreatePage<LoginViewModel, View.LoginPage>());
                await Navigation.PopToRootAsync();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
