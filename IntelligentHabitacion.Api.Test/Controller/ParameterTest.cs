using IntelligentHabitacion.Api.Controllers.V1;
using IntelligentHabitacion.Communication.Error;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace IntelligentHabitacion.Api.Test.Controller
{
    public class ParameterTest
    {
        [Fact]
        public void StringNull()
        {
            var _controller = new UserController(null);
            var result = _controller.EmailAlreadyBeenRegistered("");
            Assert.IsType<BadRequestObjectResult>(result);
            var value = (ErrorJson)((BadRequestObjectResult)result).Value;
            Assert.True(value.Errors.Count == 1);
        }

        [Fact]
        public void ParameterNull()
        {
            var _controller = new UserController(null);
            var result = _controller.Register(null);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = (ErrorJson)((BadRequestObjectResult)result).Value;
            Assert.True(value.Errors.Count == 1);
        }

        [Fact]
        public void RegisterRuleNull()
        {
            var _controller = new UserController(null);
            var result = _controller.Register(new Communication.Request.RequestRegisterUserJson());
            Assert.IsType<ObjectResult>(result);
        }
    }
}
