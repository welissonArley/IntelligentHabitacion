using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.ViewModel.DeleteAccount;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel
{
    public class UpdateUserInformationViewModel : BaseViewModel
    {
        private readonly IUserRule _userRule;

        public ICommand DeleteAccountTapped { get; }
        public ICommand ChangePasswordTapped { get; }

        public UserInformationsModel Model { get; set; }

        public UpdateUserInformationViewModel(IUserRule userRule)
        {
            _userRule = userRule;

            DeleteAccountTapped = new Command(ClickDeleteAccount);
            ChangePasswordTapped = new Command(ClickChangePasswordAccount);

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

        private void ClickDeleteAccount()
        {
            try
            {
                Navigation.PushAsync<ConfirmDeleteAccountViewModel>();
            }
            catch (System.Exception exeption)
            {
                Exception(exeption);
            }
        }

        private void ClickChangePasswordAccount()
        {
            try
            {
                Navigation.PushAsync<ChangePasswordViewModel>();
            }
            catch (System.Exception exeption)
            {
                Exception(exeption);
            }
        }
    }
}
