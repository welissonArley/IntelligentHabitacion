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
            var result = controller.Register(new RequestHomeJson());
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void CreateInvalid()
        {
            _controller.HttpContext.Request.Path = new PathString("/Home/Register/");
            var result = _controller.Register(new RequestHomeJson
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
            var result = _controller.Register(new RequestHomeJson
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

        [Fact]
        public void GetInformationsUserWithoutHome()
        {
            _controller.HttpContext.Request.Path = new PathString("/Home/Informations/");
            var result = _controller.Informations();
            Assert.IsType<BadRequestObjectResult>(result);
            var value = (ErrorJson)((BadRequestObjectResult)result).Value;
            Assert.True(value.Errors.Count == 1);
        }

        [Fact]
        public void GetInformationsSuccess()
        {
            var controller = new HomeController(new HomeFactoryFake().GetRuleLoggedUserAdministrator())
            {
                ControllerContext = GetHttpContext()
            };
            controller.HttpContext.Request.Path = new PathString("/Home/Informations/");
            var result = controller.Informations();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void UpdateInformationsInvalid()
        {
            var controller = new HomeController(new HomeFactoryFake().GetRuleLoggedUserAdministrator())
            {
                ControllerContext = GetHttpContext()
            };
            controller.HttpContext.Request.Path = new PathString("/Home/Update/");
            var result = controller.Update(new RequestHomeJson
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
        public void UpdateInformationsSuccess()
        {
            var controller = new HomeController(new HomeFactoryFake().GetRuleLoggedUserAdministrator())
            {
                ControllerContext = GetHttpContext()
            };
            controller.HttpContext.Request.Path = new PathString("/Home/Update/");
            var result = controller.Update(new RequestHomeJson
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
            Assert.IsType<OkResult>(result);
        }
    }
}
