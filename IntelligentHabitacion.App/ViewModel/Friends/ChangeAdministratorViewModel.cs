using IntelligentHabitacion.App.SetOfRules.Interface;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel.Friends.ChangeAdministrator
{
    public class ChangeAdministratorViewModel : BaseViewModel
    {
        private readonly IFriendRule _friendRule;

        public string FriendName { get; set; }
        public string FriendId { get; set; }

        public ICommand CancelCommand { get; }
        public ICommand ConfirmCommand { get; }

        public ChangeAdministratorViewModel(IFriendRule friendRule)
        {
            _friendRule = friendRule;
            CancelCommand = new Command(async () => await OnCancel());
            ConfirmCommand = new Command(async () => await OnConfirm());
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
        private async Task OnConfirm()
        {
            try
            {
                await ShowLoading();
                await _friendRule.RequestCodeToChangeAdministrator();
                await Navigation.PushAsync<ApproveActionWithCodePasswordViewModel>((viewModel, page) =>
                {
                    viewModel.FriendId = FriendId;
                    viewModel.Action = Action.ChangeAdministrator;
                });
                /*
                 * In position three it will always be the ChangeAdministratorPage
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
    }
}
