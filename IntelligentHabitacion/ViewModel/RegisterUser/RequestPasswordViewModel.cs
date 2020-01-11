using IntelligentHabitacion.Model;
using IntelligentHabitacion.SetOfRules.Interface;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.ViewModel.RegisterUser
{
    public class RequestPasswordViewModel : BaseViewModel
    {
        private readonly IUserRule _userRule;
        public ICommand OnConcludeCommand { protected set; get; }

        public RegisterUserModel Model { get; set; }

        public RequestPasswordViewModel(IUserRule userRule)
        {
            _userRule = userRule;
            OnConcludeCommand = new Command(OnConclude);
        }

        private void OnConclude()
        {
            try
            {
                _userRule.ValidatePassword(Model.Password, Model.PasswordConfirmation);

                Navigation.PopToRootAsync();
            }
            catch (System.Exception exeption)
            {
                Exception(exeption);
            }
        }
    }
}
