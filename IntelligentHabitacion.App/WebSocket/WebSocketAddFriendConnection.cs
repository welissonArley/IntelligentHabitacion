using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.WebSocket
{
    public class WebSocketAddFriendConnection
    {
        private readonly HubConnection _connection;

        public WebSocketAddFriendConnection(Action<string> callbackError)
        {
            _connection = new HubConnectionBuilder()
                    .WithUrl(new Uri("wss://c803362b.ngrok.io/addNewFriend"), HttpTransportType.WebSockets)
                    .WithAutomaticReconnect().Build();

            _connection.On<string>("ThrowError", (messageError) =>
            {
                callbackError(messageError);
            });
        }

        public async Task GetQrCodeToAddFriend(Action<string> callbackCode, Action<int> callbackTimer, string token)
        {
            _connection.On<int>("AvailableTime", (timer) =>
            {
                callbackTimer(timer);
            });
            _connection.On<string>("AvailableCode", (code) =>
            {
                callbackCode(code);
            });
            await _connection.StartAsync();
            await _connection.InvokeAsync("GetCode", token);
        }
    }
}
