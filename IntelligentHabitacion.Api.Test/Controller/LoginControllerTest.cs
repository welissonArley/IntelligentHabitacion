using IntelligentHabitacion.Api.Controllers.V1;
using IntelligentHabitacion.Api.Test.FactoryFake;
using IntelligentHabitacion.Communication.Error;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
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
            var value = (ResponseLoginJson)((OkObjectResult)result).Value;
            Assert.True(!string.IsNullOrWhiteSpace(value.Name));
            Assert.True(!value.IsPartOfOneHome);
            Assert.True(!value.IsAdministrator);
        }

        [Fact]
        public void LoginSucessUserIsAdministrator()
        {
            var request = new RequestLoginJson
            {
                User = "user4@gmail.com",
                Password = "123456"
            };

            _controller.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request)));
            var result = _controller.Login(request);

            Assert.IsType<OkObjectResult>(result);
            var value = (ResponseLoginJson)((OkObjectResult)result).Value;
            Assert.True(!string.IsNullOrWhiteSpace(value.Name));
            Assert.True(value.IsPartOfOneHome);
            Assert.True(value.IsAdministrator);
        }

        [Fact]
        public void RequestCodeResetPassword()
        {
            _controller.HttpContext.Request.Path = new PathString("/Login/RequestCodeResetPassword");
            var result = _controller.RequestCodeResetPassword("user1@gmail.com");

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void ResetYourPasswordInvalidUser()
        {
            _controller.HttpContext.Request.Path = new PathString("/Login/ResetYourPassword");
            var result = _controller.ResetYourPassword(new RequestResetYourPasswordJson
            {
                Email = "u@gmail.com"
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void ResetYourPasswordWithoutCode()
        {
            _controller.HttpContext.Request.Path = new PathString("/Login/ResetYourPassword");
            var result = _controller.ResetYourPassword(new RequestResetYourPasswordJson
            {
                Email = "user2@gmail.com"
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void ResetYourPasswordInvalidCode()
        {
            _controller.HttpContext.Request.Path = new PathString("/Login/ResetYourPassword");
            var result = _controller.ResetYourPassword(new RequestResetYourPasswordJson
            {
                Email = "user1@gmail.com",
                Code = "1"
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void ResetYourPasswordExpiredCode()
        {
            _controller.HttpContext.Request.Path = new PathString("/Login/ResetYourPassword");
            var result = _controller.ResetYourPassword(new RequestResetYourPasswordJson
            {
                Email = "user3@gmail.com",
                Code = "1234",
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void ResetYourPasswordSucess()
        {
            _controller.HttpContext.Request.Path = new PathString("/Login/ResetYourPassword");
            var result = _controller.ResetYourPassword(new RequestResetYourPasswordJson
            {
                Email = "user1@gmail.com",
                Code = "1234",
                Password = "@Password123",
                PasswordConfirmation = "@Password123"
            });

            Assert.IsType<OkResult>(result);
        }
    }
}
