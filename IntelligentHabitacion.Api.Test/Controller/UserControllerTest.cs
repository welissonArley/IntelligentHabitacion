using IntelligentHabitacion.Api.Controllers.V1;
using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.Repository.Token;
using IntelligentHabitacion.Api.Services.Interface;
using IntelligentHabitacion.Api.Services.JWT;
using IntelligentHabitacion.Api.Test.FactoryFake;
using IntelligentHabitacion.Communication.Boolean;
using IntelligentHabitacion.Communication.Error;
using IntelligentHabitacion.Communication.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace IntelligentHabitacion.Api.Test.Controller
{
    public class UserControllerTest : BaseControllerTest
    {
        private readonly UserController _controller;
        private readonly UserFactoryFake _factory;

        public UserControllerTest()
        {
            _factory = new UserFactoryFake();
            _controller = new UserController(_factory.GetRule())
            {
                ControllerContext = GetHttpContext()
            };
        }

        [Fact]
        public void UserDontExist()
        {
            _controller.HttpContext.Request.Path = new PathString("/User/EmailAlreadyBeenRegistered/");
            var result = _controller.EmailAlreadyBeenRegistered("email@dontexist.com.br");
            Assert.IsType<OkObjectResult>(result);
            var value = (BooleanJson)((OkObjectResult)result).Value;
            Assert.True(!value.Value);
        }

        [Fact]
        public void InvalidEmail()
        {
            _controller.HttpContext.Request.Path = new PathString("/User/EmailAlreadyBeenRegistered/");
            var result = _controller.EmailAlreadyBeenRegistered("invalidEmail@");
            Assert.IsType<BadRequestObjectResult>(result);
            var value = (ErrorJson)((BadRequestObjectResult)result).Value;
            Assert.True(value.Errors.Count == 1);
        }

        [Fact]
        public void UserExist()
        {
            _controller.HttpContext.Request.Path = new PathString("/User/EmailAlreadyBeenRegistered/");
            var result = _controller.EmailAlreadyBeenRegistered(_factory.UserExistFake().Email);
            Assert.IsType<OkObjectResult>(result);
            var value = (BooleanJson)((OkObjectResult)result).Value;
            Assert.True(value.Value);
        }

        [Fact]
        public void WithoutPassword()
        {
            _controller.HttpContext.Request.Path = new PathString("/User/Register/");
            var result = _controller.Register(new RequestRegisterUserJson());
            Assert.IsType<BadRequestObjectResult>(result);
            var value = (ErrorJson)((BadRequestObjectResult)result).Value;
            Assert.True(value.Errors.Count == 1);
        }

        [Fact]
        public void RegisterUserEmailExisting()
        {
            _controller.HttpContext.Request.Path = new PathString("/User/Register/");
            var result = _controller.Register(new RequestRegisterUserJson
            {
                Password = "123456",
                PasswordConfirmation = "123456",
                Email = _factory.UserExistFake().Email
            });
            Assert.IsType<BadRequestObjectResult>(result);
            var value = (ErrorJson)((BadRequestObjectResult)result).Value;
            Assert.True(value.Errors.Count == 1);
        }

        [Fact]
        public void RegisterUserInvalid()
        {
            _controller.HttpContext.Request.Path = new PathString("/User/Register/");
            var result = _controller.Register(new RequestRegisterUserJson
            {
                Password = "123456",
                PasswordConfirmation = "123456",
                Email = "newemail@email.com.br",
                Phonenumbers = new List<string> { "", "(31) 9" },
                Name = "New User",
                PushNotificationId = "Id",
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "",
                        Relationship = "",
                        Phonenumber = ""
                    },
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 2",
                        Relationship = "Relation 2",
                        Phonenumber = "37"
                    }
                }
            });
            Assert.IsType<BadRequestObjectResult>(result);
            var value = (ErrorJson)((BadRequestObjectResult)result).Value;
            Assert.True(value.Errors.Count == 6);
        }

        [Fact]
        public void RegisterUserPasswordDifferentConfirmation()
        {
            _controller.HttpContext.Request.Path = new PathString("/User/Register/");
            var result = _controller.Register(new RequestRegisterUserJson
            {
                Password = "123456",
                PasswordConfirmation = "1234567",
                Email = "newemail@email.com.br",
                Phonenumbers = new List<string> { "(31) 9 9999-9999" },
                Name = "New User",
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Relationship = "Relation 1",
                        Phonenumber = "(31) 9 9999-9999"
                    }
                }
            });
            Assert.IsType<BadRequestObjectResult>(result);
            var value = (ErrorJson)((BadRequestObjectResult)result).Value;
            Assert.True(value.Errors.Count == 1);
        }

        [Fact]
        public void RegisterUserPasswordLessThan6()
        {
            _controller.HttpContext.Request.Path = new PathString("/User/Register/");
            var result = _controller.Register(new RequestRegisterUserJson
            {
                Password = "12",
                PasswordConfirmation = "12",
                Email = "newemail@email.com.br",
                Phonenumbers = new List<string> { "(31) 9 9999-9999" },
                Name = "New User",
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Relationship = "Relation 1",
                        Phonenumber = "(31) 9 9999-9999"
                    }
                }
            });
            Assert.IsType<BadRequestObjectResult>(result);
            var value = (ErrorJson)((BadRequestObjectResult)result).Value;
            Assert.True(value.Errors.Count == 1);
        }

        [Fact]
        public void RegisterUserNullemail()
        {
            _controller.HttpContext.Request.Path = new PathString("/User/Register/");
            var result = _controller.Register(new RequestRegisterUserJson
            {
                Password = "123456",
                PasswordConfirmation = "123456",
                Email = "",
                Phonenumbers = new List<string> { "(31) 9 9999-9999" },
                Name = "New User",
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Relationship = "Relation 1",
                        Phonenumber = "(31) 9 9999-9999"
                    }
                }
            });
            Assert.IsType<BadRequestObjectResult>(result);
            var value = (ErrorJson)((BadRequestObjectResult)result).Value;
            Assert.True(value.Errors.Count == 1);
        }

        [Fact]
        public void RegisterUserSucess()
        {
            _controller.HttpContext.Request.Path = new PathString("/User/Register/");
            var request = new RequestRegisterUserJson
            {
                Password = "123456",
                PasswordConfirmation = "123456",
                Email = "newemail@email.com.br",
                Phonenumbers = new List<string> { "(31) 9 9999-9999" },
                Name = "New User",
                PushNotificationId = "Id",
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Relationship = "Relation 1",
                        Phonenumber = "(31) 9 9999-9999"
                    }
                }
            };

            var userMock = new Mock<IUserRepository>();
            userMock.Setup(c => c.GetByEmail("newemail@email.com.br")).Returns(new User
            {
                Name = "User 1",
                Email = "newemail@email.com.br",
                Password = "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413",
                Phonenumbers = new List<Phonenumber>(),
                EmergecyContacts = new List<EmergencyContact>()
            });

            var mock = new Mock<IServiceProvider>();
            mock.Setup(c => c.GetService(typeof(ITokenRepository))).Returns(GetTokenRepositoryMock());
            mock.Setup(c => c.GetService(typeof(IUserRepository))).Returns(userMock.Object);
            mock.Setup(c => c.GetService(typeof(ITokenController))).Returns(new TokenController(60));

            _controller.HttpContext.RequestServices = mock.Object;
            _controller.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request)));

            var result = _controller.Register(request);
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public void GetInformations()
        {
            _controller.HttpContext.Request.Path = new PathString("/User/Informations/");
            var result = _controller.Informations();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void ChangePasswordInvalidCurrentPassword()
        {
            _controller.HttpContext.Request.Path = new PathString("/User/ChangePassword/");
            var result = _controller.ChangePassword(new RequestChangePasswordJson
            {
                CurrentPassword = "t",
                NewPassword = "@Password123",
                NewPasswordConfirmation = "@Password123"
            });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void ChangePasswordSucess()
        {
            _controller.HttpContext.Request.Path = new PathString("/User/ChangePassword/");
            var result = _controller.ChangePassword(new RequestChangePasswordJson
            {
                CurrentPassword = "Password",
                NewPassword = "@Password123",
                NewPasswordConfirmation = "@Password123"
            });
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void UpdateInformationsInvalid()
        {
            _controller.HttpContext.Request.Path = new PathString("/User/Update/");
            _controller.HttpContext.Request.Headers.Add("Authorization", GetToken());
            var result = _controller.Update(new RequestUpdateUserJson
            {
                Name = "User",
                Email = "we",
                Phonenumbers = new List<string>(),
                EmergencyContacts = new List<RequestEmergencyContactJson>()
            });
            Assert.IsType<BadRequestObjectResult>(result);
            var value = (ErrorJson)((BadRequestObjectResult)result).Value;
            Assert.True(value.Errors.Count == 3);
        }

        [Fact]
        public void UpdateInformationsSucess()
        {
            _controller.HttpContext.Request.Path = new PathString("/User/Update/");
            var request = new RequestUpdateUserJson
            {
                Name = "User",
                Email = "user1@gmail.com",
                Phonenumbers = new List<string>
                {
                    "(31) 9 9999-9999"
                },
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact",
                        Relationship = "Mother",
                        Phonenumber = "(31) 9 8888-8888"
                    }
                }
            };
            _controller.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request)));
            var result = _controller.Update(request);
            Assert.IsType<OkResult>(result);
        }
    }
}
