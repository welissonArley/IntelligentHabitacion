using IntelligentHabitacion.Api.Controllers.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace IntelligentHabitacion.Api.Test.Controller
{
    public class GetWithErrorTest : BaseControllerTest
    {
        [Fact]
        public void UserGetInformation()
        {
            var _controller = new UserController(null)
            {
                ControllerContext = GetHttpContext()
            };
            _controller.HttpContext.Request.Path = new PathString("/User/Informations/");
            var result = _controller.Informations();
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public void RequestCodeResetPassword()
        {
            var _controller = new LoginController(null)
            {
                ControllerContext = GetHttpContext()
            };
            _controller.HttpContext.Request.Path = new PathString("/Login/RequestCodeResetPassword/");
            var result = _controller.RequestCodeResetPassword("email@email.com");
            Assert.IsType<ObjectResult>(result);
        }
    }
}
