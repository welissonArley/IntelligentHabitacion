using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.View.Modal;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel.User.Register
{
    public class RequestEmailViewModel : BaseViewModel
    {
        private readonly IUserRule _userRule;
        public ICommand NextCommand { protected set; get; }

        public ICommand WhyINeedFillThisInformationCommand { protected set; get; }

        public RegisterUserModel Model { get; set; }

        public RequestEmailViewModel(IUserRule userRule)
        {
            _userRule = userRule;
            NextCommand = new Command(async () => await OnNext());
            WhyINeedFillThisInformationCommand = new Command(async () =>
            {
                var navigation = Resolver.Resolve<INavigation>();
                await navigation.PushPopupAsync(new LgpdModal(ResourceText.DESCRIPTION_WHY_WE_NEED_YOUR_EMAIL));
            });
        }

        private async Task OnNext()
        {
            try
            {
                await ShowLoading();
                await _userRule.ValidateEmailAndVerifyIfAlreadyBeenRegistered(Model.Email);
                HideLoading();
                await Navigation.PushAsync<RequestNameViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
    }
}
