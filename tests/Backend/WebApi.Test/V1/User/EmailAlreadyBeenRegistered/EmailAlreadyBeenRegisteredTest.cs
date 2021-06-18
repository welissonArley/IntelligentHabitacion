using FluentAssertions;
using Homuai.Api;
using Homuai.Communication.Boolean;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using WebApi.Test.Builder;
using Xunit;

namespace WebApi.Test.V1.User.EmailAlreadyBeenRegistered
{
    public class EmailAlreadyBeenRegisteredTest : BaseControllersTest
    {
        public EmailAlreadyBeenRegisteredTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Validade_Sucess_Response_False()
        {
            var request = await DoGetRequest("user/email-already-been-registered/notregister@notregister.com");

            Assert.Equal(HttpStatusCode.OK, request.StatusCode);

            var response = JsonConvert.DeserializeObject<BooleanJson>(request.Content.ReadAsStringAsync().Result);

            response.Value.Should().BeFalse();
        }

        [Fact]
        public async Task Validade_Sucess_Response_True()
        {
            var request = await DoGetRequest($"user/email-already-been-registered/{EntityBuilder.UserWithoutHome.Email}");

            Assert.Equal(HttpStatusCode.OK, request.StatusCode);

            var response = JsonConvert.DeserializeObject<BooleanJson>(request.Content.ReadAsStringAsync().Result);

            response.Value.Should().BeTrue();
        }
    }
}
