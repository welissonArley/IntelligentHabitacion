using IntelligentHabitacion.Api.Controllers.V1;
using IntelligentHabitacion.Api.Test.FactoryFake;
using IntelligentHabitacion.Communication.Error;
using IntelligentHabitacion.Communication.Request;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace IntelligentHabitacion.Api.Test.Controller
{
    public class LoginControllerTest
    {
        private readonly LoginController _controller;
        private readonly LoginFactoryFake _factory;

        public LoginControllerTest()
        {
            _factory = new LoginFactoryFake();
            _controller = new LoginController(_factory.GetRule());
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
            var result = _controller.Login(new RequestLoginJson
            {
                User = "user1@gmail.com",
                Password = "123456"
            });

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
