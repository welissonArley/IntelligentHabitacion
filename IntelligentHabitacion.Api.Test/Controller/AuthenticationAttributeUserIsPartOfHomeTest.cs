using IntelligentHabitacion.Api.Filter;
using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Token;
using IntelligentHabitacion.Api.Services.JWT;
using IntelligentHabitacion.Communication.Error;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Moq;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace IntelligentHabitacion.Api.Test.Controller
{
    public class AuthenticationAttributeUserIsPartOfHomeTest
    {
        [Fact]
        public void OnActionExecuted()
        {
            var actionFiltroAutenticacao = new AuthenticationUserIsPartOfHomeAttribute(null, null, null);
            actionFiltroAutenticacao.OnActionExecuted(null);
            Assert.True(true);
        }

        [Fact]
        public void OnActionExecutingWithoutToken()
        {
            var actionFiltroAutenticacao = new AuthenticationUserIsPartOfHomeAttribute(null, null, null);

            var context = GetActionExecutingContext(true);

            actionFiltroAutenticacao.OnActionExecuting(context);
            Assert.IsType<UnauthorizedObjectResult>(context.Result);
        }

        [Fact]
        public void OnActionExecutingInvalidUser()
        {
            var userMock = new Mock<IUserRepository>();
            userMock.Setup(c => c.GetByEmail(It.IsAny<string>())).Returns(new Repository.Model.User());

            var actionFiltroAutenticacao = new AuthenticationUserIsPartOfHomeAttribute(userMock.Object, null, new TokenController(60));

            var context = GetActionExecutingContext();

            actionFiltroAutenticacao.OnActionExecuting(context);
            Assert.IsType<UnauthorizedObjectResult>(context.Result);
        }

        [Fact]
        public void OnActionExecutingDifferentToken()
        {
            var userMock = new Mock<IUserRepository>();
            userMock.Setup(c => c.GetByEmail(It.IsAny<string>())).Returns(new Repository.Model.User
            {
                Id = 1,
                HomeAssociationId = 1,
                HomeAssociation = new Repository.Model.HomeAssociation
                {
                    Home = new Repository.Model.Home
                    {
                        AdministratorId = 1
                    }
                }
            });

            var tokenMock = new Mock<ITokenRepository>();
            tokenMock.Setup(c => c.Get(It.IsAny<long>())).Returns(new Token
            {
                Value = "0"
            });

            var actionFiltroAutenticacao = new AuthenticationUserIsPartOfHomeAttribute(userMock.Object, tokenMock.Object, new TokenController(60));

            var context = GetActionExecutingContext();

            actionFiltroAutenticacao.OnActionExecuting(context);
            Assert.IsType<UnauthorizedObjectResult>(context.Result);
        }

        [Fact]
        public void OnActionExecutingTokenExpired()
        {
            var actionFiltroAutenticacao = new AuthenticationUserIsPartOfHomeAttribute(null, null, new TokenController(0.1)) ;

            var context = GetActionExecutingContext(expirationTimeMinutes: 0.01);

            Thread.Sleep(2000);

            actionFiltroAutenticacao.OnActionExecuting(context);
            Assert.IsType<UnauthorizedObjectResult>(context.Result);

            var result = (ErrorJson)((UnauthorizedObjectResult)context.Result).Value;
            Assert.True(result.ErrorCode == ErrorCode.TokenExpired);
        }

        private ActionExecutingContext GetActionExecutingContext(bool withoutToken = false, double expirationTimeMinutes = 60)
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = withoutToken ? "" : $"Basic {new TokenController(expirationTimeMinutes).CreateToken("u@u.com")}";

            return new ActionExecutingContext(new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
                ActionDescriptor = new ControllerActionDescriptor
                {
                    ControllerName = ""
                }
            }, new List<IFilterMetadata>(), new Dictionary<string, object>(), null);
        }
    }
}
