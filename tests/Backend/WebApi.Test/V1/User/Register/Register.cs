using Homuai.Api;
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
        }
    }
}
