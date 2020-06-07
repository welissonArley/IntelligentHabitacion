using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.Useful;
using IntelligentHabitacion.App.View.Modal;
using IntelligentHabitacion.App.View.Modal.MenuOptions;
using IntelligentHabitacion.App.ViewModel.Friends.ChangeAdministrator;
using Rg.Plugins.Popup.Extensions;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel.Friends
{
    public class FriendDetailsViewModel : BaseViewModel
    {
        private readonly IFriendRule _friendRule;

        public ICommand MakePhonecallCommand { protected set; get; }
        public ICommand NotifyFriendOrderHasArrivedCommand { protected set; get; }
        public ICommand MenuOptionsCommand { protected set; get; }

        private ICommand ChangeDateJoinOnCommand { set; get; }
        private ICommand ChangeAdministratorCommand { set; get; }

        public FriendModel Model { get; set; }
        public ICommand RefreshCallback { get; set; }

        public FriendDetailsViewModel(IFriendRule friendRule)
        {
            MakePhonecallCommand = new Command(async (value) =>
            {
                await MakeCall(value.ToString());
            });
            MenuOptionsCommand = new Command(async () =>
            {
                await ShowAdministratorOptions();
            });

            ChangeDateJoinOnCommand = new Command(async () =>
            {
                await ChangeDateOption();
            });
            ChangeAdministratorCommand = new Command(async () =>
            {
                await ChangeAdministrator();
            });

            NotifyFriendOrderHasArrivedCommand = new Command(async() =>
            {
                await NotifyFriendOrderHasArrived();
            });

            _friendRule = friendRule;
        }

        private async Task MakeCall(string number)
        {
            await ShowLoading();
            Phonecall.Make(number);
            HideLoading();
        }

        private async Task ShowAdministratorOptions()
        {
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PushPopupAsync(new AdministratorFriendDetailModal(ChangeDateJoinOnCommand, ChangeAdministratorCommand));
        }

        private async Task ChangeDateOption()
        {
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PopAllPopupAsync();
            await ShowLoading();
            await navigation.PushPopupAsync(new Calendar(Model.JoinedOn, OnDateSelected, maximumDate: DateTime.Today));
            HideLoading();
        }
        private async Task ChangeAdministrator()
        {
            try
            {
                var navigation = Resolver.Resolve<INavigation>();
                await navigation.PopAllPopupAsync();
                await ShowLoading();
                await Navigation.PushAsync<ChangeAdministratorViewModel>((viewModel, page) =>
                {
                    viewModel.FriendName = Model.Name;
                    viewModel.FriendId = Model.Id;
                });
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }

        private async Task OnDateSelected(DateTime date)
        {
            try
            {
                await ShowLoading();
                var friend = await _friendRule.ChangeDateJoinOn(Model.Id, date);
                Model.DescriptionDateJoined = friend.DescriptionDateJoined;
                Model.JoinedOn = friend.JoinedOn;
                OnPropertyChanged(new PropertyChangedEventArgs("Model"));
                RefreshCallback?.Execute(null);
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }

        private async Task NotifyFriendOrderHasArrived()
        {
            try
            {
                await ShowLoading();
                await _friendRule.NotifyFriendOrderHasArrived(Model.Id);
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
