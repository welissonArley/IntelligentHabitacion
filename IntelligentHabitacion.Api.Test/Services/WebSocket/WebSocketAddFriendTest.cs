using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.Services.JWT;
using IntelligentHabitacion.Api.Services.WebSocket.AddFriend;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using Microsoft.AspNetCore.SignalR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace IntelligentHabitacion.Api.Test.Services.WebSocket
{
    public class WebSocketAddFriendTest
    {
        private readonly WebSocketAddFriendHub _hub;
        private readonly string _connectionAdminId;
        private readonly string _connectionFriendId;
        private readonly Mock<IHubCallerClients> mockClients;
        private readonly Mock<IClientProxy> mockClientProxy;

        public WebSocketAddFriendTest()
        {
            _connectionAdminId = "123456789";
            _connectionFriendId = "789456123";

            Mock<HubCallerContext> hubCallerContext = new Mock<HubCallerContext>();
            hubCallerContext.Setup(c => c.ConnectionId).Returns(_connectionAdminId);
            mockClients = new Mock<IHubCallerClients>();
            mockClientProxy = new Mock<IClientProxy>();

            mockClients.Setup(clients => clients.Client(It.IsAny<string>())).Returns(mockClientProxy.Object);

            Mock<IHubContext<WebSocketAddFriendHub>> mockHubContext = new Mock<IHubContext<WebSocketAddFriendHub>>();

            Mock<IHubClients> hubClients = new Mock<IHubClients>();
            hubClients.Setup(clients => clients.Client(It.IsAny<string>())).Returns(mockClientProxy.Object);
            mockHubContext.Setup(c => c.Clients).Returns(hubClients.Object);

            _hub = new WebSocketAddFriendHub(mockHubContext.Object, GetRule())
            {
                Clients = mockClients.Object,
                Context = hubCallerContext.Object
            };
        }

        [Fact]
        public async void GetCodeNullToken()
        {
            await _hub.GetCode(null);

            mockClients.Verify(clients => clients.Client(_connectionAdminId), Times.Once);

            mockClientProxy.Verify(clientProxy => clientProxy.SendCoreAsync("ThrowError",
                It.Is<object[]>(o => o != null && o.Length == 1 && o[0].Equals(ResourceTextException.UNKNOW_ERROR)), System.Threading.CancellationToken.None), Times.Once);
        }

        [Fact]
        public async void GetCodeUserIsNotAdmin()
        {
            await _hub.GetCode(new TokenController(2).CreateToken("notAdmin@notAdmin.com"));

            mockClients.Verify(clients => clients.Client(_connectionAdminId), Times.Once);

            mockClientProxy.Verify(clientProxy => clientProxy.SendCoreAsync("ThrowError",
                It.Is<object[]>(o => o != null && o.Length == 1 && o[0].Equals(ResourceTextException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE)), System.Threading.CancellationToken.None), Times.Once);
        }

        [Fact]
        public async void CodeWasReadNull()
        {
            await _hub.CodeWasRead(null, null);

            mockClients.Verify(clients => clients.Client(_connectionAdminId), Times.Once);

            mockClientProxy.Verify(clientProxy => clientProxy.SendCoreAsync("ThrowError",
                It.Is<object[]>(o => o != null && o.Length == 1 && o[0].Equals(ResourceTextException.UNKNOW_ERROR)), System.Threading.CancellationToken.None), Times.Once);
        }

        [Fact]
        public async void UserWithHome()
        {
            await _hub.CodeWasRead(new TokenController(2).CreateToken("userWithHome@userWithHome.com"), null);

            mockClients.Verify(clients => clients.Client(_connectionAdminId), Times.Once);

            mockClientProxy.Verify(clientProxy => clientProxy.SendCoreAsync("ThrowError",
                It.Is<object[]>(o => o != null && o.Length == 1 && o[0].Equals(ResourceTextException.USER_IS_PART_OF_A_HOME)), System.Threading.CancellationToken.None), Times.Once);
        }

        [Fact]
        public async void UserWithInvalidCode()
        {
            await _hub.CodeWasRead(new TokenController(2).CreateToken("friend@friend.com"), null);

            mockClients.Verify(clients => clients.Client(_connectionAdminId), Times.Once);

            mockClientProxy.Verify(clientProxy => clientProxy.SendCoreAsync("ThrowError",
                It.Is<object[]>(o => o != null && o.Length == 1 && o[0].Equals(ResourceTextException.CODE_INVALID)), System.Threading.CancellationToken.None), Times.Once);
        }

        [Fact]
        public async void CompleteSucessTest()
        {
            await _hub.GetCode(new TokenController(2).CreateToken("admin@admin.com"));

            mockClients.Verify(clients => clients.Client(_connectionAdminId), Times.Once);

            mockClientProxy.Verify(clientProxy => clientProxy.SendCoreAsync("AvailableCode", It.Is<object[]>(o => o != null && o.Length == 1 && !string.IsNullOrWhiteSpace(o[0].ToString())), System.Threading.CancellationToken.None), Times.Once);

            var hubCallerContextFriend = new Mock<HubCallerContext>();
            hubCallerContextFriend.Setup(c => c.ConnectionId).Returns(_connectionFriendId);
            _hub.Context = hubCallerContextFriend.Object;

            await _hub.CodeWasRead(new TokenController(2).CreateToken("friend@friend.com"), "tokenToAddFriend");

            mockClients.Verify(clients => clients.Client(_connectionAdminId), Times.Exactly(2));

            mockClientProxy.Verify(clientProxy => clientProxy.SendCoreAsync("CodeWasRead", It.Is<object[]>(o => o != null && o.Length == 1 && o[0] is ResponseFriendJson), System.Threading.CancellationToken.None), Times.Once);

            hubCallerContextFriend = new Mock<HubCallerContext>();
            hubCallerContextFriend.Setup(c => c.ConnectionId).Returns(_connectionAdminId);
            _hub.Context = hubCallerContextFriend.Object;

            await _hub.Approve(null);

            mockClients.Verify(clients => clients.Client(_connectionAdminId), Times.Exactly(3));

            mockClientProxy.Verify(clientProxy => clientProxy.SendCoreAsync("ThrowError", It.Is<object[]>(o => o != null && o.Length == 1 && o[0].Equals(ResourceTextException.UNKNOW_ERROR)), System.Threading.CancellationToken.None), Times.Once);

            await _hub.Approve(new RequestApproveAddFriendJson { JoinedOn = DateTime.Today, MonthlyRent = -1 });

            mockClients.Verify(clients => clients.Client(_connectionAdminId), Times.Exactly(4));

            mockClientProxy.Verify(clientProxy => clientProxy.SendCoreAsync("ThrowError", It.Is<object[]>(o => o != null && o.Length == 1 && o[0].Equals(ResourceTextException.MONTHLYRENT_INVALID)), System.Threading.CancellationToken.None), Times.Once);

            await _hub.Approve(new RequestApproveAddFriendJson { JoinedOn = DateTime.Today, MonthlyRent = 750 });

            mockClients.Verify(clients => clients.Client(_connectionAdminId), Times.Exactly(5));
            mockClients.Verify(clients => clients.Client(_connectionFriendId), Times.Once);

            await _hub.OnDisconnectedAsync(null);
        }

        [Fact]
        public async void CompleteDeclineTest()
        {
            await _hub.GetCode(new TokenController(2).CreateToken("admin@admin.com"));

            mockClients.Verify(clients => clients.Client(_connectionAdminId), Times.Once);

            mockClientProxy.Verify(clientProxy => clientProxy.SendCoreAsync("AvailableCode", It.Is<object[]>(o => o != null && o.Length == 1 && !string.IsNullOrWhiteSpace(o[0].ToString())), System.Threading.CancellationToken.None), Times.Once);

            var hubCallerContextFriend = new Mock<HubCallerContext>();
            hubCallerContextFriend.Setup(c => c.ConnectionId).Returns(_connectionFriendId);
            _hub.Context = hubCallerContextFriend.Object;

            await _hub.CodeWasRead(new TokenController(2).CreateToken("friend@friend.com"), "tokenToAddFriend");

            mockClients.Verify(clients => clients.Client(_connectionAdminId), Times.Exactly(2));

            mockClientProxy.Verify(clientProxy => clientProxy.SendCoreAsync("CodeWasRead", It.Is<object[]>(o => o != null && o.Length == 1 && o[0] is ResponseFriendJson), System.Threading.CancellationToken.None), Times.Once);

            hubCallerContextFriend = new Mock<HubCallerContext>();
            hubCallerContextFriend.Setup(c => c.ConnectionId).Returns(_connectionAdminId);
            _hub.Context = hubCallerContextFriend.Object;

            await _hub.Decline();
        }

        [Fact]
        public async void DisconnectOnAddingFriendTest()
        {
            await _hub.GetCode(new TokenController(2).CreateToken("admin@admin.com"));

            mockClients.Verify(clients => clients.Client(_connectionAdminId), Times.Once);

            mockClientProxy.Verify(clientProxy => clientProxy.SendCoreAsync("AvailableCode", It.Is<object[]>(o => o != null && o.Length == 1 && !string.IsNullOrWhiteSpace(o[0].ToString())), System.Threading.CancellationToken.None), Times.Once);

            var hubCallerContextFriend = new Mock<HubCallerContext>();
            hubCallerContextFriend.Setup(c => c.ConnectionId).Returns(_connectionFriendId);
            _hub.Context = hubCallerContextFriend.Object;

            await _hub.CodeWasRead(new TokenController(2).CreateToken("friend@friend.com"), "tokenToAddFriend");

            mockClients.Verify(clients => clients.Client(_connectionAdminId), Times.Exactly(2));

            hubCallerContextFriend = new Mock<HubCallerContext>();
            hubCallerContextFriend.Setup(c => c.ConnectionId).Returns(_connectionAdminId);
            _hub.Context = hubCallerContextFriend.Object;

            await _hub.OnDisconnectedAsync(null);

            mockClientProxy.Verify(clientProxy => clientProxy.SendCoreAsync("ThrowError", It.Is<object[]>(o => o != null && o.Length == 1 && o[0].Equals(ResourceTextException.CONNECTION_ADMINISTRATOR_LOST)), System.Threading.CancellationToken.None), Times.Once);
        }

        [Fact]
        public async void TestingTime()
        {
            var mockClientProxy = new Mock<IClientProxy>();
            Mock<IHubContext<WebSocketAddFriendHub>> mockHubContext = new Mock<IHubContext<WebSocketAddFriendHub>>();
            Mock<IHubClients> hubClients = new Mock<IHubClients>();
            hubClients.Setup(clients => clients.Client(It.IsAny<string>())).Returns(mockClientProxy.Object);
            mockHubContext.Setup(c => c.Clients).Returns(hubClients.Object);

            var context = new Context(mockHubContext.Object, "TestTimer");
            var props = typeof(Context).GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static).First(c => c.Name.Equals("_secondTimer"));
            context.StartTimer();
            props.SetValue(context, (short)1, null);
            await Task.Delay(3000);

            mockClientProxy.Verify(clientProxy => clientProxy.SendCoreAsync("AvailableTime", It.Is<object[]>(o => o != null && o.Length == 1), System.Threading.CancellationToken.None), Times.Exactly(2));
        }

        private AddFriendRule GetRule()
        {
            return new AddFriendRule(TokenMock(), GetRepository(), new TokenController(60));
        }
        private IUserRepository GetRepository()
        {
            var repositorioMock = new Mock<IUserRepository>();
            repositorioMock.Setup(c => c.GetByEmail("notAdmin@notAdmin.com")).Returns(new User());
            repositorioMock.Setup(c => c.GetByEmail("admin@admin.com")).Returns(new User
            {
                Id = 1,
                HomeAssociationId = 1,
                HomeAssociation = new HomeAssociation
                {
                    Id = 1,
                    Home = new Home
                    {
                        AdministratorId = 1
                    }
                }
            });

            repositorioMock.Setup(c => c.GetByEmail("userWithHome@userWithHome.com")).Returns(new User
            {
                Id = 1,
                HomeAssociationId = 1
            });
            repositorioMock.Setup(c => c.GetByEmail("friend@friend.com")).Returns(new User
            {
                Id = 2,
                Name = "Friend",
                Phonenumbers = new List<Phonenumber>
                {
                    new Phonenumber
                    {
                        Number = "(37) 8 9689-9856"
                    }
                },
                EmergecyContacts = new List<EmergencyContact>
                {
                    new EmergencyContact
                    {
                        Phonenumber =  "(37) 8 9689-9856"
                    }
                }
            });
            repositorioMock.Setup(c => c.GetById(1)).Returns(new User { Id = 1 });
            repositorioMock.Setup(c => c.GetById(2)).Returns(new User { Id = 2 });
            
            repositorioMock.Setup(c => c.GetHomeId(1)).Returns(1);

            return repositorioMock.Object;
        }
        private ICodeRepository TokenMock()
        {
            var mock = new Mock<ICodeRepository>();
            mock.Setup(c => c.GetByUser(1)).Returns(new List<Code>{ new Code() });
            mock.Setup(c => c.GetByCode("tokenToAddFriend")).Returns(new Code { UserId = 1 });

            return mock.Object;
        }
    }
}
