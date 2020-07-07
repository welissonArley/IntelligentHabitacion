﻿using IntelligentHabitacion.App.SetOfRules.Interface;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel
{
    public enum Action
    {
        ChangeAdministrator = 0,
        RemoveFriend = 1,
        DeleteHome = 2
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
        private readonly IHomeRule _homeRule;

        public ApproveActionWithCodePasswordViewModel(IFriendRule friendRule, IHomeRule homeRule)
        {
            _friendRule = friendRule;
            _homeRule = homeRule;
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
                    case Action.DeleteHome:
                        {
                            await _homeRule.Delete(ConfirmationCode, Password);
                            FunctionCallbackCommand?.Execute(null);
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
