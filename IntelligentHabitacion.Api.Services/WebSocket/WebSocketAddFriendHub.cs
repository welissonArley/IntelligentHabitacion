using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Services.WebSocket
{
    public class WebSocketAddFriendHub : Hub
    {
        private readonly IHubContext<WebSocketAddFriendHub> _hubContext;
        private readonly IFriendRule _friendRule;

        public WebSocketAddFriendHub(IHubContext<WebSocketAddFriendHub> hubContext, IFriendRule friendRule)
        {
            _hubContext = hubContext;
            _friendRule = friendRule;
        }

        public override Task OnDisconnectedAsync(System.Exception exception)
        {
            Disconnect();
            return base.OnDisconnectedAsync(exception);
        }

        public async Task GetCode(string userToken)
        {
            try
            {
                var response = _friendRule.GetCodeToAddFriend(userToken);
                var code = response.Item1;
                var admin = response.Item2;
                Context.Items.Add(Context.ConnectionId, new AddFriendController(_hubContext, Context.ConnectionId));
                await Clients.Client(Context.ConnectionId).SendAsync("AvailableCode", code);
            }
            catch (IntelligentHabitacionException e)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("ThrowError", e.Message);
            }
            catch
            {
                await Clients.Client(Context.ConnectionId).SendAsync("ThrowError", ResourceTextException.UNKNOW_ERROR);
            }
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
