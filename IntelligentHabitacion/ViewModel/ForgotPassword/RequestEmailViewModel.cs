using IntelligentHabitacion.Model;
using IntelligentHabitacion.SetOfRules.Interface;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.ViewModel.ForgotPassword
{
    public class RequestEmailViewModel : BaseViewModel
    {
        private readonly ILoginRule _loginRule;
        public ICommand RequestCodeCommand { protected set; get; }

        public ForgetPasswordModel Model { get; set; }

        public RequestEmailViewModel(ILoginRule loginRule)
        {
            _loginRule = loginRule;
            RequestCodeCommand = new Command(OnRequestCode);
        }

        private void OnRequestCode()
        {
            try
            {
                _loginRule.RequestCode(Model.Email);

                Navigation.PushAsync<ChangePasswordViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                Exception(exeption);
            }
        }
    }
}
