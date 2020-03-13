using IntelligentHabitacion.Api.Controllers.V1;
using IntelligentHabitacion.Api.Test.FactoryFake;
using IntelligentHabitacion.Communication.Boolean;
using IntelligentHabitacion.Communication.Error;
using IntelligentHabitacion.Communication.Request;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace IntelligentHabitacion.Api.Test.Controller
{
    public class UserControllerTest
    {
        private readonly UserController _controller;
        private readonly UserFactoryFake _factory;

        public UserControllerTest()
        {
            _factory = new UserFactoryFake();
            _controller = new UserController(_factory.GetRule());
        }

        [Fact]
        public void UserDontExist()
        {
            var result = _controller.EmailAlreadyBeenRegistered("email@dontexist.com.br");
            Assert.IsType<OkObjectResult>(result);
            var value = (BooleanJson)((OkObjectResult)result).Value;
            Assert.True(!value.Value);
        }

        [Fact]
        public void InvalidEmail()
        {
            var result = _controller.EmailAlreadyBeenRegistered("invalidEmail@");
            Assert.IsType<BadRequestObjectResult>(result);
            var value = (ErrorJson)((BadRequestObjectResult)result).Value;
            Assert.True(value.Errors.Count == 1);
        }

        [Fact]
        public void UserExist()
        {
            var result = _controller.EmailAlreadyBeenRegistered(_factory.UserExistFake().Email);
            Assert.IsType<OkObjectResult>(result);
            var value = (BooleanJson)((OkObjectResult)result).Value;
            Assert.True(value.Value);
        }

        [Fact]
        public void WithoutPassword()
        {
            var result = _controller.Register(new RequestRegisterUserJson());
            Assert.IsType<BadRequestObjectResult>(result);
            var value = (ErrorJson)((BadRequestObjectResult)result).Value;
            Assert.True(value.Errors.Count == 1);
        }

        [Fact]
        public void RegisterUserEmailExisting()
        {
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
            var result = _controller.Register(new RequestRegisterUserJson
            {
                Password = "123456",
                PasswordConfirmation = "123456",
                Email = "newemail@email.com.br",
                Phonenumbers = new List<string> { "", "(31) 9" },
                Name = "New User",
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "",
                        DegreeOfKinship = "",
                        Phonenumbers = new List<string>()
                    },
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 2",
                        DegreeOfKinship = "Relation 2",
                        Phonenumbers = new List<string>{ "" }
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
                        DegreeOfKinship = "Relation 1",
                        Phonenumbers = new List<string>{ "(31) 9 9999-9999" }
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
                        DegreeOfKinship = "Relation 1",
                        Phonenumbers = new List<string>{ "(31) 9 9999-9999" }
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
                        DegreeOfKinship = "Relation 1",
                        Phonenumbers = new List<string>{ "(31) 9 9999-9999" }
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
            var result = _controller.Register(new RequestRegisterUserJson
            {
                Password = "123456",
                PasswordConfirmation = "123456",
                Email = "newemail@email.com.br",
                Phonenumbers = new List<string> { "(31) 9 9999-9999" },
                Name = "New User",
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        DegreeOfKinship = "Relation 1",
                        Phonenumbers = new List<string>{ "(31) 9 9999-9999" }
                    }
                }
            });
            Assert.IsType<CreatedResult>(result);
        }
    }
}
