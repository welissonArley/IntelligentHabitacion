using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Token;
using IntelligentHabitacion.Api.SetOfRules.JWT;
using IntelligentHabitacion.Api.SetOfRules.LoggedUser;
using IntelligentHabitacion.Api.Test.FactoryFake;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;

namespace IntelligentHabitacion.Api.Test.Controller
{
    public class BaseControllerTest
    {
        protected ControllerContext GetHttpContext()
        {
            return new ControllerContext()
            {
                HttpContext = new DefaultHttpContext
                {
                    RequestServices = GetServiceProviderMock()
                }
            };
        }

        private IServiceProvider GetServiceProviderMock()
        {
            var userFactory = new UserFactoryFake();

            var mock = new Mock<IServiceProvider>();
            mock.Setup(c => c.GetService(typeof(ILoggedUser))).Returns(userFactory.GetLoggedUser());
            mock.Setup(c => c.GetService(typeof(ITokenRepository))).Returns(GetTokenRepositoryMock());
            mock.Setup(c => c.GetService(typeof(IUserRepository))).Returns(userFactory.GetRepository());

            return mock.Object;
        }

        protected ITokenRepository GetTokenRepositoryMock()
        {
            var repositorioMock = new Mock<ITokenRepository>();
            repositorioMock.Setup(c => c.Create(new Token()));
            repositorioMock.Setup(c => c.Get(It.IsAny<long>())).Returns(new Token());

            return repositorioMock.Object;
        }

        protected string GetToken()
        {
            return $"Basic {new TokenController().CreateToken("user1@gmail.com")}";
        }
    }
}
