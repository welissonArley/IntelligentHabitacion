using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.View.Modal;
using IntelligentHabitacion.App.WebSocket;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using Rg.Plugins.Popup.Extensions;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel.Friends.Add
{
    public class AcceptNewFriendViewModel : BaseViewModel
    {
        private WebSocketAddFriendConnection _webSocketAddFriendConnection;
        public WebSocketAddFriendConnection WebSocketAddFriendConnection
        {
            get
            {
                return _webSocketAddFriendConnection;
            }
            set
            {
                _webSocketAddFriendConnection = value;

                var callbackWhenAnErrorOccurs = new Command(async (message) =>
                {
                    await HandleException(message.ToString());
                });
                var callbackTimeChanged = new Command(async (time) =>
                {
                    await OnChangedTime((int)time);
                });

                _webSocketAddFriendConnection.SetCallbacks(callbackWhenAnErrorOccurs, callbackTimeChanged);
            }
        }
        public AcceptNewFriendModel Model { get; set; }
        public ResponseFriendJson NewFriendToAddJson { get; set; }
        public string Time { get; set; }

        public ICommand ApprovedOperation { get; set; }

        public ICommand SelectEntryDateTapped { get; }
        public ICommand CancelOperationTapped { get; }
        public ICommand ApproveOperationTapped { get; }

        public AcceptNewFriendViewModel()
        {
            SelectEntryDateTapped = new Command(async () =>
            {
                await ClickSelectDueDate();
            });
            CancelOperationTapped = new Command(async () =>
            {
                await OnCancelOperation();
            });
            ApproveOperationTapped = new Command(async () =>
            {
                await OnApproveOperation();
            });

            /*
             * In position two it will always be the QrCodeToAddFriendPage
             */
            var navigation = Resolver.Resolve<INavigation>();
            navigation.RemovePage(navigation.NavigationStack[2]);
        }

        private async Task OnChangedTime(int timer)
        {
            if (timer > 0)
            {
                Time = DateTime.Today.AddSeconds(timer).ToString("mm:ss");
                OnPropertyChanged(new PropertyChangedEventArgs("Time"));
            }
            else
            {
                DisconnectFromSocket();
                await HandleException(ResourceText.TITLE_TIME_EXPIRED_TRY_AGAIN);
            }
        }
        private async Task HandleException(string message)
        {
            await Navigation.PopAsync();
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PushPopupAsync(new ErrorModal(message));
        }

        private async Task ClickSelectDueDate()
        {
            await ShowLoading();
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PushPopupAsync(new Calendar(Model.EntryDate, OnDateSelected, maximumDate: DateTime.Today));
            HideLoading();
        }
        private Task OnDateSelected(DateTime date)
        {
            Model.EntryDate = date;
            OnPropertyChanged(new PropertyChangedEventArgs("Model"));
            return Task.CompletedTask;
        }

        private async Task OnCancelOperation()
        {
            await _webSocketAddFriendConnection.DeclinedFriendCandidate();
            DisconnectFromSocket();
            await Navigation.PopAsync();
        }
        private async Task OnApproveOperation()
        {
            var navigation = Resolver.Resolve<INavigation>();
            if (Model.MonthlyRent <= 0)
                await navigation.PushPopupAsync(new ErrorModal(ResourceTextException.MONTHLYRENT_INVALID));
            else
            {
                await ShowLoading();
                await _webSocketAddFriendConnection.ApproveFriendCandidate(new Command(async () =>
                {
                    await navigation.PushPopupAsync(new OperationSuccessfullyExecutedModal(ResourceText.TITLE_ACCEPTED));
                    NewFriendToAddJson.JoinedOn = Model.EntryDate;
                    ApprovedOperation?.Execute(NewFriendToAddJson);
                    await Task.Delay(1100);
                    DisconnectFromSocket();
                    await navigation.PopAllPopupAsync();
                    await Navigation.PopAsync();
                }), new Communication.Request.RequestApproveAddFriendJson
                {
                    JoinedOn = Model.EntryDate,
                    MonthlyRent = Model.MonthlyRent
                });
                HideLoading();
            }
        }

        public void DisconnectFromSocket()
        {
            Task.Run(async () => await _webSocketAddFriendConnection.StopConnection());
        }
    }
}
