using IntelligentHabitacion.Api.Controllers.V1;
using IntelligentHabitacion.Api.Test.FactoryFake;
using IntelligentHabitacion.Communication.Error;
using IntelligentHabitacion.Communication.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace IntelligentHabitacion.Api.Test.Controller
{
    public class HomeControllerTest : BaseControllerTest
    {
        private readonly HomeController _controller;

        public HomeControllerTest()
        {
            _controller = new HomeController(new HomeFactoryFake().GetRule())
            {
                ControllerContext = GetHttpContext()
            };
        }

        [Fact]
        public void CreateUserAlreadyPartOfHome()
        {
            var controller = new HomeController(new HomeFactoryFake().GetRuleLoggedUserAdministrator())
            {
                ControllerContext = GetHttpContext()
            };
            controller.HttpContext.Request.Path = new PathString("/Home/Register/");
            var result = controller.Register(new RequestRegisterHomeJson());
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void CreateInvalid()
        {
            _controller.HttpContext.Request.Path = new PathString("/Home/Register/");
            var result = _controller.Register(new RequestRegisterHomeJson
            {
                Address = "",
                City = new RequestRegisterCityJson
                {
                    Name = "City",
                    State = new RequestRegisterStateJson
                    {
                        Name = "State",
                        Country = new RequestRegisterCountryJson
                        {
                            Name = "Country",
                            Abbreviation = "A"
                        }
                    }
                },
                Complement = "",
                Neighborhood = "Neighborhood",
                NetworksName = "",
                NetworksPassword = "password",
                Number = "1",
                ZipCode = ""
            });
            Assert.IsType<BadRequestObjectResult>(result);
            var value = (ErrorJson)((BadRequestObjectResult)result).Value;
            Assert.True(value.Errors.Count == 3);
        }

        [Fact]
        public void CreateSucess()
        {
            _controller.HttpContext.Request.Path = new PathString("/Home/Register/");
            var result = _controller.Register(new RequestRegisterHomeJson
            {
                Address = "Address",
                City = new RequestRegisterCityJson
                {
                    Name = "City",
                    State = new RequestRegisterStateJson
                    {
                        Name = "State",
                        Country = new RequestRegisterCountryJson
                        {
                            Name = "Country",
                            Abbreviation = "A"
                        }
                    }
                },
                Complement = "",
                Neighborhood = "Neighborhood",
                NetworksName = "Network",
                NetworksPassword = "password",
                Number = "1",
                ZipCode = "00.000-000"
            });
            Assert.IsType<CreatedResult>(result);
        }
    }
}
