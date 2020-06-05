using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.Services.JWT;
using IntelligentHabitacion.Api.SetOfRules.LoggedUser;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace IntelligentHabitacion.Api.Test.Controller
{
    public class LoggedUserTest
    {
        [Fact]
        public void GetLoggedUser()
        {
            var mockUser = new Mock<IUserRepository>();
            var entityUser = new User
            {
                Email = "teste@email.com",
                Name = "Teste",
                EmergecyContacts = new List<EmergencyContact>(),
                Password = "@Password",
                Phonenumbers = new List<Phonenumber>()
            };
            entityUser.Encrypt();
            mockUser.Setup(c => c.GetByEmail(It.IsAny<string>())).Returns(entityUser);

            var mockHttpContext = new Mock<IHttpContextAccessor>();

            var context = new DefaultHttpContext();
            var tokenController = new TokenController(60);
            context.Request.Headers.Add("Authorization", $"Basic {tokenController.CreateToken("user@gmail.com")}");

            mockHttpContext.Setup(c => c.HttpContext).Returns(context);

            var controller = new LoggedUser(mockHttpContext.Object, mockUser.Object, tokenController);
            var user = controller.User();
            user.Decrypt();
            Assert.Equal("teste@email.com", user.Email);
            user = controller.User();
            Assert.Equal("teste@email.com", user.Email);
        }
    }
}
