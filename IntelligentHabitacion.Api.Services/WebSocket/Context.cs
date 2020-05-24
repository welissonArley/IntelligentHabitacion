using IntelligentHabitacion.Exception;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Timers;

namespace IntelligentHabitacion.Api.Services.WebSocket
{
    public class Context
    {
        private readonly IHubContext<WebSocketAddFriendHub> _hubContext;
        private string _adminConnectionSocketId { get; set; }
        private string _newFriendId { get; set; }
        private string _newFriendConnectionSocketId { get; set; }

        private short _secondTimer { get; set; }
        private Timer _timer { get; set; }

        public Context(IHubContext<WebSocketAddFriendHub> hubContext, string adminConnectionSocketId)
        {
            _hubContext = hubContext;
            _adminConnectionSocketId = adminConnectionSocketId;
        }

        public string GetAdminConnectionSocketId()
        {
            return _adminConnectionSocketId;
        }
        public async Task SendErrorConnectionLostFriendCandidate()
        {
            if (!string.IsNullOrWhiteSpace(_newFriendConnectionSocketId))
                await _hubContext.Clients.Client(_newFriendConnectionSocketId).SendAsync("ThrowError", ResourceTextException.CONNECTION_ADMINISTRATOR_LOST);
        }
        public async Task SendDeclinedFriendCandidate()
        {
            await _hubContext.Clients.Client(_newFriendConnectionSocketId).SendAsync("Declined");
            _newFriendConnectionSocketId = null;
        }
        public string GetFriendId()
        {
            return _newFriendId;
        }
        public string GetFriendConnectionSocketId()
        {
            return _newFriendConnectionSocketId;
        }
        public void SetNewFriendInformations(string newFriendId, string newFriendConnectionSocketId)
        {
            _newFriendId = newFriendId;
            _newFriendConnectionSocketId = newFriendConnectionSocketId;
        }

        public void StopTimer()
        {
            _timer?.Stop();
            _timer?.Dispose();
            _timer = null;
        }
        public void StartTimer()
        {
            _secondTimer = 60;
            _timer = new Timer(1000)
            {
                Enabled = false
            };
            _timer.Elapsed += ElapsedTimer;
            _timer.Enabled = true;
        }
        private async void ElapsedTimer(object sender, ElapsedEventArgs e)
        {
            if (_secondTimer >= 0)
                await _hubContext.Clients.Client(_adminConnectionSocketId).SendAsync("AvailableTime", _secondTimer--);
            else
                StopTimer();
        }
    }
}
