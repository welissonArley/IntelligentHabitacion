using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.SQLite.Interface;
using IntelligentHabitacion.App.ViewModel.DeleteAccount;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel
{
    public class UpdateUserInformationViewModel : BaseViewModel
    {
        private readonly IUserRule _userRule;
        private readonly ISqliteDatabase _database;

        public ICommand DeleteAccountTapped { get; }
        public ICommand ChangePasswordTapped { get; }
        public ICommand LogoutTapped { get; }
        public ICommand UpdateInformationsTapped { get; }

        public UserInformationsModel Model { get; set; }

        public UpdateUserInformationViewModel(IUserRule userRule, ISqliteDatabase database)
        {
            _database = database;
            _userRule = userRule;

            DeleteAccountTapped = new Command(async () => await ClickDeleteAccount());
            ChangePasswordTapped = new Command(async () => await ClickChangePasswordAccount());
            LogoutTapped = new Command(async () => await ClickLogoutAccount());
            UpdateInformationsTapped = new Command(async () => await ClickUpdateInformations());

            Model = new UserInformationsModel
            {
                Name = "Welisson",
                Email = "welisson@gmail.com",
                PhoneNumber1 = "(31) 9 8565-0000",
                PhoneNumber2 = "",
                EmergencyContact1 = new EmergencyContactModel
                {
                    Name = "Z",
                    FamilyRelationship = "Mãe",
                    PhoneNumber = "(37) 9 5555-5555"
                },
                EmergencyContact2 = new EmergencyContactModel
                {
                    Name = "J",
                    FamilyRelationship = "Pai",
                    PhoneNumber = "(37) 9 7777-7777"
                }
            };
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

        private async Task ClickLogoutAccount()
        {
            try
            {
                _database.Delete();
                Application.Current.MainPage = new NavigationPage((Page)XLabs.Forms.Mvvm.ViewFactory.CreatePage<LoginViewModel, View.LoginPage>());
                await Navigation.PopToRootAsync();
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
                _userRule.UpdateInformations(Model);
                await Navigation.PopAsync();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
