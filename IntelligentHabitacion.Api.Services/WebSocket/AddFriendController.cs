using Microsoft.AspNetCore.SignalR;
using System.Timers;

namespace IntelligentHabitacion.Api.Services.WebSocket
{
    public class AddFriendController
    {
        private readonly IHubContext<WebSocketAddFriendHub> _hubContext;

        private short _secondTimer { get; set; }
        private Timer _timer { get; set; }
        private readonly string _connectionSocketId;

        public AddFriendController(IHubContext<WebSocketAddFriendHub> hubContext, string connectionSocketId)
        {
            _hubContext = hubContext;
            _secondTimer = 60;
            _connectionSocketId = connectionSocketId;

            _timer = new Timer(1000)
            {
                Enabled = false
            };
            _timer.Elapsed += ElapsedTimer;
            _timer.Enabled = true;
        }

        public void StopProcess()
        {
            _timer.Stop();
            _secondTimer = 60;
        }

        private async void ElapsedTimer(object sender, ElapsedEventArgs e)
        {
            if (_secondTimer >= 0)
                await _hubContext.Clients.Client(_connectionSocketId).SendAsync("AvailableTime", _secondTimer--);
            else
                StopProcess();
        }
    }
}
