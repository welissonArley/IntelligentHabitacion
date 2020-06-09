using IntelligentHabitacion.App.SetOfRules.Interface;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel.Friends.ChangeAdministrator
{
    public class RemoveFriendFromHomeViewModel : BaseViewModel
    {
        private readonly IFriendRule _friendRule;

        public string FriendName { get; set; }
        public string FriendId { get; set; }

        public ICommand CancelCommand { get; }
        public ICommand ConfirmCommand { get; }

        public ICommand FunctionCallbackCommand { get; set; }

        public RemoveFriendFromHomeViewModel(IFriendRule friendRule)
        {
            _friendRule = friendRule;
            CancelCommand = new Command(async () => await OnCancel());
            ConfirmCommand = new Command(async () => await OnConfirm());
        }
        private async Task OnConfirm()
        {
            try
            {
                await ShowLoading();
                await _friendRule.RequestCodeToRemoveFriend();
                await Navigation.PushAsync<ApproveActionWithCodePasswordViewModel>((viewModel, page) =>
                {
                    viewModel.FriendId = FriendId;
                    viewModel.Action = Action.RemoveFriend;
                    viewModel.FunctionCallbackCommand = FunctionCallbackCommand;
                });
                /*
                 * In position three it will always be the RemoveFriendFromHomePage
                 */
                var navigation = Resolver.Resolve<INavigation>();
                navigation.RemovePage(navigation.NavigationStack[3]);
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
        private async Task OnCancel()
        {
            try
            {
                await Navigation.PopAsync();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
