using IntelligentHabitacion.App.SetOfRules.Interface;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        private readonly IUserRule _userRule;

        public ICommand ChangePasswordTapped { get; }

        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string PasswordConfirmation { get; set; }

        public ChangePasswordViewModel(IUserRule userRule)
        {
            _userRule = userRule;
            ChangePasswordTapped = new Command(async () => await ClickChangePasswordAccount());
        }

        private async Task ClickChangePasswordAccount()
        {
            try
            {
                await ShowLoading();
                await _userRule.ChangePassword(CurrentPassword, NewPassword, PasswordConfirmation);
                await Navigation.PopAsync();
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
    }
}
