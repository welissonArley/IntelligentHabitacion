using Microsoft.AspNetCore.SignalR;
using System;
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

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Disconnect();
            return base.OnDisconnectedAsync(exception);
        }

        public async Task GetCode(string userToken)
        {
            Context.Items.Add(Context.ConnectionId, new AddFriendController(_hubContext, Context.ConnectionId));
            await Clients.Client(Context.ConnectionId).SendAsync("AvailableCode", "codigpgerado");
        }

        private void Disconnect()
        {
            if (Context.Items.ContainsKey(Context.ConnectionId))
            {
                var controller = (AddFriendController)Context.Items[Context.ConnectionId];
                controller.StopProcess();
                Context.Items.Remove(controller);
            }
        }
    }
}
