using IntelligentHabitacion.App.SQLite.Interface;
using IntelligentHabitacion.App.WebSocket;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.Friends.Add
{
    public class QrCodeToAddFriendViewModel : BaseViewModel
    {
        private readonly WebSocketAddFriendConnection _webSocketAddFriendConnection;
        public string Time { get; set; }

        public QrCodeToAddFriendViewModel(ISqliteDatabase database)
        {
            _webSocketAddFriendConnection = new WebSocketAddFriendConnection();
            Task.Run(() => Device.BeginInvokeOnMainThread(async () => await _webSocketAddFriendConnection.GetQrCodeToAddFriend(OnChangedTime, database.Get().Token)));
        }

        private async void OnChangedTime(int timer)
        {
            if (timer > 0)
            {
                Time = DateTime.Today.AddSeconds(timer).ToString("mm:ss");
                OnPropertyChanged(new PropertyChangedEventArgs("Time"));
            }
            else
                await Navigation.PopToRootAsync();
        }
    }
}
