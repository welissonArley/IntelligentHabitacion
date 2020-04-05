using IntelligentHabitacion.Api.Filter;
using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Token;
using IntelligentHabitacion.Api.SetOfRules.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace IntelligentHabitacion.Api.Test.Controller
{
    public class AuthenticationAttributeTest
    {
        [Fact]
        public void OnActionExecuted()
        {
            var actionFiltroAutenticacao = new AuthenticationAttribute(null, null);
            actionFiltroAutenticacao.OnActionExecuted(null);
            Assert.True(true);
        }

        [Fact]
        public void OnActionExecutingWithoutToken()
        {
            var actionFiltroAutenticacao = new AuthenticationAttribute(null, null);

            var context = GetActionExecutingContext(true);

            actionFiltroAutenticacao.OnActionExecuting(context);
            Assert.IsType<UnauthorizedObjectResult>(context.Result);
        }

        [Fact]
        public void OnActionExecutingError()
        {
            var actionFiltroAutenticacao = new AuthenticationAttribute(null, null);

            var context = GetActionExecutingContext();

            actionFiltroAutenticacao.OnActionExecuting(context);
            Assert.IsType<UnauthorizedObjectResult>(context.Result);
        }

        [Fact]
        public void OnActionExecutingInvalidUser()
        {
            var userMock = new Mock<IUserRepository>();

            var actionFiltroAutenticacao = new AuthenticationAttribute(userMock.Object, null);

            var context = GetActionExecutingContext();

            actionFiltroAutenticacao.OnActionExecuting(context);
            Assert.IsType<UnauthorizedObjectResult>(context.Result);
        }

        [Fact]
        public void OnActionExecutingDifferentToken()
        {
            var userMock = new Mock<IUserRepository>();
            userMock.Setup(c => c.GetByEmail(It.IsAny<string>())).Returns(new Repository.Model.User());

            var tokenMock = new Mock<ITokenRepository>();
            tokenMock.Setup(c => c.Get(It.IsAny<long>())).Returns(new Token
            {
                Value = "0"
            });

            var actionFiltroAutenticacao = new AuthenticationAttribute(userMock.Object, tokenMock.Object);

            var context = GetActionExecutingContext();

            actionFiltroAutenticacao.OnActionExecuting(context);
            Assert.IsType<UnauthorizedObjectResult>(context.Result);
        }

        private ActionExecutingContext GetActionExecutingContext(bool withoutToken = false)
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = withoutToken ? "" : $"Basic {new TokenController().CreateToken("u@u.com")}";

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
