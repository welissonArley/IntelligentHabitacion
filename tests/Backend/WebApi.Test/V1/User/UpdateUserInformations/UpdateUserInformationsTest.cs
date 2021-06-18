using Homuai.Api;
using System.Net;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Request;
using WebApi.Test.Builder;
using Xunit;

namespace WebApi.Test.V1.User.UpdateUserInformations
{
    public class UpdateUserInformationsTest : BaseControllersTest
    {
        public UpdateUserInformationsTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Validade_Sucess()
        {
            var token = EntityBuilder.Token_UserWithoutHome;

            var json = RequestUpdateUser.Instance().Build();

            var request = await DoPutRequest("user", json, token);

            Assert.Equal(HttpStatusCode.OK, request.StatusCode);
        }

        [Fact]
        public async Task Validade_EmptyName()
        {
            var token = EntityBuilder.Token_UserWithoutHome;

            var json = RequestUpdateUser.Instance().Build();
            json.Name = "";

            var request = await DoPutRequest("user", json, token);

            Assert.Equal(HttpStatusCode.BadRequest, request.StatusCode);
        }
    }
}
