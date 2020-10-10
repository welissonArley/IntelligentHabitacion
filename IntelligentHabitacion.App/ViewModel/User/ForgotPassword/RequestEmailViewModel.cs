using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.User.ForgotPassword
{
    public class RequestEmailViewModel : BaseViewModel
    {
        private readonly ILoginRule _loginRule;
        public ICommand RequestCodeCommand { protected set; get; }

        public ForgetPasswordModel Model { get; set; }

        public RequestEmailViewModel(ILoginRule loginRule)
        {
            _loginRule = loginRule;
            RequestCodeCommand = new Command(async () => await OnRequestCode());
        }

        private async Task OnRequestCode()
        {
            try
            {
                await ShowLoading();
                await _loginRule.RequestCode(Model.Email);
                HideLoading();
                await Navigation.PushAsync<ResetPasswordViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
    }
}
