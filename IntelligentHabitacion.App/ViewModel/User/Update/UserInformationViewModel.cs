using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.ViewModel.Login;
using IntelligentHabitacion.App.ViewModel.User.Delete;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.User.Update
{
    public class UserInformationViewModel : BaseViewModel
    {
        private readonly IUserRule _userRule;
        private readonly UserPreferences _userPreferences;

        public ICommand DeleteAccountTapped { get; }
        public ICommand ChangePasswordTapped { get; }
        public ICommand UpdateInformationsTapped { get; }

        public UserInformationsModel Model { get; set; }

        public UserInformationViewModel(IUserRule userRule, UserPreferences userPreferences)
        {
            _userPreferences = userPreferences;
            _userRule = userRule;

            DeleteAccountTapped = new Command(async () => await ClickDeleteAccount());
            ChangePasswordTapped = new Command(async () => await ClickChangePasswordAccount());
            UpdateInformationsTapped = new Command(async () => await ClickUpdateInformations());

            Model = Task.Run(async () => await _userRule.GetInformations()).Result;
        }

        private async Task ClickDeleteAccount()
        {
            try
            {
                await Navigation.PushAsync<ConfirmDeleteAccountViewModel>();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        private async Task ClickChangePasswordAccount()
        {
            try
            {
                await Navigation.PushAsync<ChangePasswordViewModel>();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        private async Task ClickUpdateInformations()
        {
            try
            {
                await ShowLoading();
                await _userRule.UpdateInformations(Model);
                _userPreferences.SaveUserInformations(Model.Name, Model.Email);
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
