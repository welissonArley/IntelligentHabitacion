using IntelligentHabitacion.Api.Controllers.V1;
using IntelligentHabitacion.Api.Test.FactoryFake;
using IntelligentHabitacion.Communication.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
