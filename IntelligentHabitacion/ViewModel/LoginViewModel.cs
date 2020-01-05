using IntelligentHabitacion.SetOfRules.Interface;
using IntelligentHabitacion.ViewModel.ForgotPassword;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ILoginRule _loginRule;

        public ICommand LoginCommand { protected set; get; }
        public ICommand RegisterCommand { protected set; get; }
        public ICommand ForgotPasswordCommand { protected set; get; }

        public string Email { get; set; }
        public string Password { get; set; }
        
        public LoginViewModel(ILoginRule loginRule)
        {
            _loginRule = loginRule;
            LoginCommand = new Command(OnLogin);
            RegisterCommand = new Command(OnRegister);
            ForgotPasswordCommand = new Command(OnForgotPassword);
        }

        private void OnLogin()
        {
            try
            {
                _loginRule.Login(Email, Password);
            }
            catch(System.Exception exeption)
            {
                Exception(exeption);
            }
        }
        private void OnRegister()
        {
            try
            {
                
            }
            catch (System.Exception exeption)
            {
                Exception(exeption);
            }
        }
        private void OnForgotPassword()
        {
            try
            {
                Navigation.PushAsync<RequestEmailViewModel>((viewModel, page) => viewModel.Model = new Model.ForgetPasswordModel());
            }
            catch (System.Exception exeption)
            {
                Exception(exeption);
            }
        }
    }
}
