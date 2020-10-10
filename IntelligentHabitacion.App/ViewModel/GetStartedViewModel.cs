using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.ViewModel.Login;
using IntelligentHabitacion.App.ViewModel.User.Register;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel
{
    public class GetStartedViewModel : BaseViewModel
    {
        public ICommand LoginCommand { protected set; get; }
        public ICommand RegisterCommand { protected set; get; }

        public GetStartedViewModel(UserPreferences userPreferences)
        {
            LoginCommand = new Command(async () => await OnLogin());
            RegisterCommand = new Command(async () => await OnRegister());
        }

        private async Task OnRegister()
        {
            try
            {
                await Navigation.PushAsync<RequestEmailViewModel>((viewModel, page) => viewModel.Model = new Model.RegisterUserModel());
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private async Task OnLogin()
        {
            try
            {
                await Navigation.PushAsync<LoginViewModel>();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
