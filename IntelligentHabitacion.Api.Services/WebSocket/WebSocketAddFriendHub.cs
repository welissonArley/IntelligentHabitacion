using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Services.WebSocket
{
    public class WebSocketAddFriendHub : Hub
    {
        private readonly IHubContext<WebSocketAddFriendHub> _hubContext;

        public WebSocketAddFriendHub(IHubContext<WebSocketAddFriendHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task GetQrCode(string userToken)
        {
            Context.Items.Add(Context.ConnectionId, new AddFriendController(_hubContext, Context.ConnectionId));
        }
    }
}
