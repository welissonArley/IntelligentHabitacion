using IntelligentHabitacion.Api.Services.Interface;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Services.WebSocket.AddFriend
{
    public class WebSocketAddFriendHub : Hub
    {
        private readonly IHubContext<WebSocketAddFriendHub> _hubContext;
        private readonly IAddFriendRule _friendRule;

        public WebSocketAddFriendHub(IHubContext<WebSocketAddFriendHub> hubContext, IAddFriendRule friendRule)
        {
            _hubContext = hubContext;
            _friendRule = friendRule;
        }

        public override async Task OnDisconnectedAsync(System.Exception exception)
        {
            await Disconnect();
            await base.OnDisconnectedAsync(exception);
        }

        public async Task GetCode(string userToken)
        {
            try
            {
                var response = _friendRule.GetCodeToAddFriend(userToken);
                var context = new Context(_hubContext, Context.ConnectionId);
                Manager.Add(Context.ConnectionId, response.AdminId, context);
                context.StartTimer();
                await Clients.Client(Context.ConnectionId).SendAsync("AvailableCode", response.Code);
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

        public async Task CodeWasRead(string userToken, string code)
        {
            try
            {
                var response = _friendRule.CodeWasRead(userToken, code);
                var context = Manager.Get(response.AdminId);
                context.StopTimer();
                context.SetNewFriendInformations(response.Id, Context.ConnectionId);
                context.StartTimer();

                var informationsNewFriendToAdd = (ResponseFriendJson)response;
                await Clients.Client(context.GetAdminConnectionSocketId()).SendAsync("CodeWasRead", informationsNewFriendToAdd);
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

        public async Task Decline()
        {
            var adminId = Manager.GetAdminId(Context.ConnectionId);
            if (!string.IsNullOrWhiteSpace(adminId))
            {
                var context = Manager.Get(adminId);
                context.StopTimer();
                await context.SendDeclinedFriendCandidate();
                await Manager.Remove(Context.ConnectionId);
            }
        }

        public async Task Approve(RequestApproveAddFriendJson requestApprove)
        {
            try
            {
                var adminId = Manager.GetAdminId(Context.ConnectionId);
                if (!string.IsNullOrWhiteSpace(adminId))
                {
                    var context = Manager.Get(adminId);
                    context.StopTimer();
                    var friendId = context.GetFriendId();
                    var connetionFriend = context.GetFriendConnectionSocketId();
                    _friendRule.ApproveFriend(adminId, friendId, requestApprove);
                    context.SetNewFriendInformations(null, null);
                    await Clients.Client(Context.ConnectionId).SendAsync("SuccessfullyApproved");
                    await Clients.Client(connetionFriend).SendAsync("SuccessfullyApproved");
                    await Manager.Remove(Context.ConnectionId);
                }
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

        private async Task Disconnect()
        {
            await Manager.Remove(Context.ConnectionId);
        }
    }
}
