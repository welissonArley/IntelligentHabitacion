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
    public class RequestPhoneNumberViewModel : BaseViewModel
    {
        private readonly IUserRule _userRule;
        public ICommand NextCommand { protected set; get; }
        public ICommand WhyINeedFillThisInformationCommand { protected set; get; }

        public RegisterUserModel Model { get; set; }

        public RequestPhoneNumberViewModel(IUserRule userRule)
        {
            _userRule = userRule;
            NextCommand = new Command(async () => await OnNext());
            WhyINeedFillThisInformationCommand = new Command(async () =>
            {
                var navigation = Resolver.Resolve<INavigation>();
                await navigation.PushPopupAsync(new LgpdModal(ResourceText.DESCRIPTION_WHY_WE_NEED_YOUR_TELEPHONE_NUMBER));
            });
        }

        private async Task OnNext()
        {
            try
            {
                _userRule.ValidatePhoneNumber(Model.PhoneNumber1, Model.PhoneNumber2);

                await Navigation.PushAsync<RequestEmergencyContact1ViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
