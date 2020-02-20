using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.ForgotPassword
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
