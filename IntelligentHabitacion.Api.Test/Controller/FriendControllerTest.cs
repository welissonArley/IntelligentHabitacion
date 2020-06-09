using IntelligentHabitacion.Api.Controllers.V1;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.Test.FactoryFake;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;

namespace IntelligentHabitacion.Api.Test.Controller
{
    public class FriendControllerTest : BaseControllerTest
    {
        private readonly FriendController _controller;

        public FriendControllerTest()
        {
            _controller = new FriendController(new FriendFactoryFake().GetRule())
            {
                ControllerContext = GetHttpContext()
            };
        }

        [Fact]
        public void UserWithoutHome()
        {
            _controller.HttpContext.Request.Path = new PathString("/Friend/Friends/");
            var result = _controller.Friends();
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UserWithoutFriends()
        {
            var controller = new FriendController(new FriendFactoryFake().GetRuleLoggedUserWithoutFriend())
            {
                ControllerContext = GetHttpContext()
            };
            controller.HttpContext.Request.Path = new PathString("/Home/Register/");
            var result = controller.Friends();
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void GetFriendsSuccess()
        {
            var controller = new FriendController(new FriendFactoryFake().GetRuleLoggedUserAdministrator())
            {
                ControllerContext = GetHttpContext()
            };
            controller.HttpContext.Request.Path = new PathString("/Home/Register/");
            var result = controller.Friends();
            Assert.IsType<OkObjectResult>(result);

            var value = (List<ResponseFriendJson>)((OkObjectResult)result).Value;

            Assert.True(value.Count == 1);
        }

        [Fact]
        public void ChangeDateJoinHome_FriendNotFound()
        {
            _controller.HttpContext.Request.Path = new PathString("/Friend/ChangeDateJoinHome/");
            var result = _controller.ChangeDateJoinHome(new RequestChangeDateJoinHomeJson
            {
                FriendId = new User().EncryptedId()
            });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void ChangeDateJoinHome_FriendInvalid()
        {
            _controller.HttpContext.Request.Path = new PathString("/Friend/ChangeDateJoinHome/");
            var result = _controller.ChangeDateJoinHome(new RequestChangeDateJoinHomeJson
            {
                FriendId = new User() { Id = 1 }.EncryptedId()
            });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void ChangeDateJoinHome_Sucess()
        {
            var controller = new FriendController(new FriendFactoryFake().GetRuleLoggedUserAdministrator())
            {
                ControllerContext = GetHttpContext()
            };
            controller.HttpContext.Request.Path = new PathString("/Friend/ChangeDateJoinHome/");
            var result = controller.ChangeDateJoinHome(new RequestChangeDateJoinHomeJson
            {
                FriendId = new User() { Id = 2 }.EncryptedId(),
                JoinOn = DateTime.Today
            });
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void NotifyOrderHasArrived_FriendNotFound()
        {
            _controller.HttpContext.Request.Path = new PathString("/Friend/NotifyOrderReceived/");
            var result = _controller.NotifyOrderReceived(new User().EncryptedId());
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void NotifyOrderHasArrived_FriendInvalid()
        {
            _controller.HttpContext.Request.Path = new PathString("/Friend/NotifyOrderReceived/");
            var result = _controller.NotifyOrderReceived(new User() { Id = 1 }.EncryptedId());
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void NotifyOrderHasArrived_Sucess()
        {
            var controller = new FriendController(new FriendFactoryFake().GetRuleLoggedUserAdministrator())
            {
                ControllerContext = GetHttpContext()
            };
            controller.HttpContext.Request.Path = new PathString("/Friend/NotifyOrderReceived/");
            var result = controller.NotifyOrderReceived(new User() { Id = 2 }.EncryptedId());
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void RequestCodeChangeAdministrator_Sucess()
        {
            _controller.HttpContext.Request.Path = new PathString("/Friend/RequestCodeChangeAdministrator/");
            var result = _controller.RequestCodeChangeAdministrator();
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void RequestCodeChangeAdministrator_Error()
        {
            var controller = new FriendController(null)
            {
                ControllerContext = GetHttpContext()
            };
            controller.HttpContext.Request.Path = new PathString("/Friend/RequestCodeChangeAdministrator/");
            var result = controller.RequestCodeChangeAdministrator();
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public void ChangeAdministrator_FriendNotFound()
        {
            _controller.HttpContext.Request.Path = new PathString("/Friend/ChangeAdministrator/");
            var result = _controller.ChangeAdministrator(new RequestAdminActionsOnFriendJson
            {
                FriendId = new User().EncryptedId()
            });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void ChangeAdministrator_FriendInvalid()
        {
            _controller.HttpContext.Request.Path = new PathString("/Friend/ChangeAdministrator/");
            var result = _controller.ChangeAdministrator(new RequestAdminActionsOnFriendJson
            {
                FriendId = new User() { Id = 1 }.EncryptedId()
            });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void ChangeAdministrator_InvalidPassword()
        {
            var controller = new FriendController(new FriendFactoryFake().GetRuleLoggedUserAdministrator())
            {
                ControllerContext = GetHttpContext()
            };
            controller.HttpContext.Request.Path = new PathString("/Friend/ChangeAdministrator/");
            var result = controller.ChangeAdministrator(new RequestAdminActionsOnFriendJson
            {
                Password = "0",
                FriendId = new User() { Id = 2 }.EncryptedId()
            });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void ChangeAdministrator_InvalidCode()
        {
            var controller = new FriendController(new FriendFactoryFake().GetRuleLoggedUserAdministrator())
            {
                ControllerContext = GetHttpContext()
            };
            controller.HttpContext.Request.Path = new PathString("/Friend/ChangeAdministrator/");
            var result = controller.ChangeAdministrator(new RequestAdminActionsOnFriendJson
            {
                Password = "Password",
                Code = "0",
                FriendId = new User() { Id = 2 }.EncryptedId()
            });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void ChangeAdministrator_ExpiredCode()
        {
            var controller = new FriendController(new FriendFactoryFake().GetRuleLoggedUserAdministratorTokenExpired())
            {
                ControllerContext = GetHttpContext()
            };
            controller.HttpContext.Request.Path = new PathString("/Friend/ChangeAdministrator/");
            var result = controller.ChangeAdministrator(new RequestAdminActionsOnFriendJson
            {
                Password = "Password",
                Code = "1234",
                FriendId = new User() { Id = 2 }.EncryptedId()
            });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void ChangeAdministrator_Sucess()
        {
            var controller = new FriendController(new FriendFactoryFake().GetRuleLoggedUserAdministrator())
            {
                ControllerContext = GetHttpContext()
            };
            controller.HttpContext.Request.Path = new PathString("/Friend/ChangeAdministrator/");
            var result = controller.ChangeAdministrator(new RequestAdminActionsOnFriendJson
            {
                Password = "Password",
                Code = "1234",
                FriendId = new User() { Id = 2 }.EncryptedId()
            });
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void RemoveFriend_ExpiredCode()
        {
            var controller = new FriendController(new FriendFactoryFake().GetRuleLoggedUserAdministratorTokenExpired())
            {
                ControllerContext = GetHttpContext()
            };
            controller.HttpContext.Request.Path = new PathString("/Friend/RemoveFriend/");
            var result = controller.RemoveFriend(new RequestAdminActionsOnFriendJson
            {
                Password = "Password",
                Code = "1234",
                FriendId = new User() { Id = 2 }.EncryptedId()
            });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void RequestCodeRemoveFriend_Error()
        {
            var controller = new FriendController(null)
            {
                ControllerContext = GetHttpContext()
            };
            controller.HttpContext.Request.Path = new PathString("/Friend/RemoveFriend/");
            var result = controller.RequestCodeRemoveFriend();
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public void RequestCodeRemoveFriend_Sucess()
        {
            _controller.HttpContext.Request.Path = new PathString("/Friend/RemoveFriend/");
            var result = _controller.RequestCodeRemoveFriend();
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void RemoveFriend_Sucess()
        {
            var controller = new FriendController(new FriendFactoryFake().GetRuleLoggedUserAdministrator())
            {
                ControllerContext = GetHttpContext()
            };
            controller.HttpContext.Request.Path = new PathString("/Friend/RemoveFriend/");
            var result = controller.RemoveFriend(new RequestAdminActionsOnFriendJson
            {
                Password = "Password",
                Code = "1234",
                FriendId = new User() { Id = 2 }.EncryptedId()
            });
            Assert.IsType<OkResult>(result);
        }
    }
}
