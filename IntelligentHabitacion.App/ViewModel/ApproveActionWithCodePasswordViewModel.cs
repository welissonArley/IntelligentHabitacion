using IntelligentHabitacion.App.SetOfRules.Interface;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel
{
    public enum Action
    {
        ChangeAdministrator = 0,
        RemoveFriend = 1
    }

    public class ApproveActionWithCodePasswordViewModel : BaseViewModel
    {
        public string FriendId { get; set; }
        public Action Action { get; set; }

        public string ConfirmationCode { get; set; }
        public string Password { get; set; }
        public ICommand ConfirmCommand { get; }

        public ICommand FunctionCallbackCommand { get; set; }

        private readonly IFriendRule _friendRule;

        public ApproveActionWithCodePasswordViewModel(IFriendRule friendRule)
        {
            _friendRule = friendRule;
            ConfirmCommand = new Command(async () =>
            {
                await Confirm();
            });
        }

        private async Task Confirm()
        {
            try
            {
                await ShowLoading();
                switch (Action)
                {
                    case Action.ChangeAdministrator:
                        {
                            await _friendRule.ChangeAdministrator(ConfirmationCode, FriendId, Password);
                            await Navigation.PopAsync();
                        }
                        break;
                    case Action.RemoveFriend:
                        {
                            await _friendRule.RemoveFriend(ConfirmationCode, FriendId, Password);
                            await Navigation.PopAsync();
                            FunctionCallbackCommand?.Execute(FriendId);
                        }
                        break;
                }
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
