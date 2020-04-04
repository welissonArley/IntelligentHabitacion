using IntelligentHabitacion.Api.Controllers.V1;
using IntelligentHabitacion.Api.Test.FactoryFake;
using IntelligentHabitacion.Communication.Error;
using IntelligentHabitacion.Communication.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using Xunit;

namespace IntelligentHabitacion.Api.Test.Controller
{
    public class LoginControllerTest : BaseControllerTest
    {
        private readonly LoginController _controller;

        public LoginControllerTest()
        {
            _controller = new LoginController(new LoginFactoryFake().GetRule())
            {
                ControllerContext = GetHttpContext()
            };
            _controller.HttpContext.Request.Path = new PathString("/Login");
        }

        [Fact]
        public void LoginUserDontExist()
        {
            var result = _controller.Login(new RequestLoginJson
            {
                User = "dontexist@gmail.com",
                Password = "123456"
            });

            Assert.IsType<UnauthorizedObjectResult>(result);
            var value = (ErrorJson)((UnauthorizedObjectResult)result).Value;
            Assert.True(value.Errors.Count == 1);
        }

        [Fact]
        public void LoginSucess()
        {
            var request = new RequestLoginJson
            {
                User = "user1@gmail.com",
                Password = "123456"
            };

            _controller.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request)));
            var result = _controller.Login(request);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
