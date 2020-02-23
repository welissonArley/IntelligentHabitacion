using IntelligentHabitacion.App.SetOfRules.Interface;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.DeleteAccount
{
    public class DeleteAccountViewModel : BaseViewModel
    {
        private readonly IUserRule _userRule;

        public string ConfirmationCode { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public ICommand ConfirmCommand { protected set; get; }

        public DeleteAccountViewModel(IUserRule userRule)
        {
            _userRule = userRule;
            ConfirmCommand = new Command(OnConfirm);
            Email = "br***@gmail.com";
        }

        private void OnConfirm()
        {
            try
            {
                _userRule.DeleteAccount(ConfirmationCode, Password);
                Application.Current.MainPage = new NavigationPage((Page)XLabs.Forms.Mvvm.ViewFactory.CreatePage<LoginViewModel, View.LoginPage>());
                Navigation.PopToRootAsync();
            }
            catch (System.Exception exeption)
            {
                Exception(exeption);
            }
        }
    }
}
