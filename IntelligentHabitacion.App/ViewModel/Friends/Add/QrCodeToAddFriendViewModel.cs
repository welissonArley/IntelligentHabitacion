using IntelligentHabitacion.App.SQLite.Interface;
using IntelligentHabitacion.App.View.Modal;
using IntelligentHabitacion.App.WebSocket;
using Rg.Plugins.Popup.Extensions;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel.Friends.Add
{
    public class QrCodeToAddFriendViewModel : BaseViewModel
    {
        private readonly WebSocketAddFriendConnection _webSocketAddFriendConnection;
        public string Time { get; set; }
        public bool ReceivedCode { get; set; }
        public string QrCode { get; set; }

        public ICommand CancelOperationTapped { get; set; }

        public QrCodeToAddFriendViewModel(ISqliteDatabase database)
        {
            _webSocketAddFriendConnection = new WebSocketAddFriendConnection(HandleException);
            CancelOperationTapped = new Command(async() =>
            {
                await OnCancelOperation();
            });
            Task.Run(() => Device.BeginInvokeOnMainThread(async () => await _webSocketAddFriendConnection.GetQrCodeToAddFriend(OnCodeReceived, OnChangedTime, database.Get().Token)));
        }

        private void OnCodeReceived(string code)
        {
            ReceivedCode = true;
            QrCode = code;
            OnPropertyChanged(new PropertyChangedEventArgs("QrCode"));
            OnPropertyChanged(new PropertyChangedEventArgs("ReceivedCode"));
        }

        private void OnChangedTime(int timer)
        {
            if (timer > 0)
            {
                Time = DateTime.Today.AddSeconds(timer).ToString("mm:ss");
                OnPropertyChanged(new PropertyChangedEventArgs("Time"));
            }
            else
            {
                DisconnectFromSocket();
                HandleException(ResourceText.TITLE_TIME_EXPIRED_TRY_AGAIN);
            }
        }

        private async Task OnCancelOperation()
        {
            await Navigation.PopToRootAsync();
            DisconnectFromSocket();
        }

        private async void HandleException(string message)
        {
            await Navigation.PopToRootAsync();
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PushPopupAsync(new ErrorModal(message));
        }

        public void DisconnectFromSocket()
        {
            Task.Run(async () => await _webSocketAddFriendConnection.StopConnection());
        }
    }
}
