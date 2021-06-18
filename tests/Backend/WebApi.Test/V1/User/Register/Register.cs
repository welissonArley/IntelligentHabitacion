using FluentAssertions;
using Homuai.Api;
using Homuai.Communication.Response;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Request;
using Xunit;

namespace WebApi.Test.V1.User.Register
{
    public class Register : BaseControllersTest
    {
        public Register(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Validade_Sucess()
        {
            var user = RequestRegisterUser.Instance().Build();

            var request = await DoPostRequest("user", user);

            Assert.Equal(HttpStatusCode.Created, request.StatusCode);

            var response = JsonConvert.DeserializeObject<ResponseUserRegisteredJson>(request.Content.ReadAsStringAsync().Result);

            response.Id.Should().NotBeNullOrWhiteSpace();
            response.ProfileColor.Should().NotBeNullOrWhiteSpace();
        }
    }
}
