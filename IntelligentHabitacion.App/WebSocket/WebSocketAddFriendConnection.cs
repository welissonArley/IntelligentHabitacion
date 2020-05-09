using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.WebSocket
{
    public class WebSocketAddFriendConnection
    {
        private readonly HubConnection _connection;

        public WebSocketAddFriendConnection()
        {
            _connection = new HubConnectionBuilder()
                    .WithUrl(new Uri("wss://f75e4008.ngrok.io/addNewFriend"), HttpTransportType.WebSockets)
                    .WithAutomaticReconnect().Build();
        }

        public async Task GetQrCodeToAddFriend(Action<int> callbackTimer, string token)
        {
            _connection.On<int>("AvailableTime", (timer) =>
            {
                callbackTimer(timer);
            });
            await _connection.StartAsync();
            await _connection.InvokeAsync("GetQrCode", token);
        }
    }
}
