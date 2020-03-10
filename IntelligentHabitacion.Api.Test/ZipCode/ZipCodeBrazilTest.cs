using IntelligentHabitacion.Communication;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using System;
using Xunit;

namespace IntelligentHabitacion.Api.Test.ZipCode
{
    public class ZipCodeBrazilTest
    {
        private readonly IntelligentHabitacionHttpClient _httpClient;

        public ZipCodeBrazilTest()
        {
            _httpClient = new IntelligentHabitacionHttpClient();
        }

        [Fact]
        public void InvalidZipCode()
        {
            Assert.ThrowsAsync<ZipCodeInvalidException>(async () => await _httpClient.GetLocationBrazilByZipCode("00.000-000"));
        }

        [Fact]
        public async void ValidZipCode()
        {
            var result = await _httpClient.GetLocationBrazilByZipCode("01001000");
            Assert.IsType<ResponseLocationBrazilJson>(result);
            Assert.NotEmpty(result.Cep);
            Assert.NotEmpty(result.Logradouro);
            Assert.NotEmpty(result.Complemento);
            Assert.NotEmpty(result.Bairro);
            Assert.NotEmpty(result.Localidade);
            Assert.IsType<string>(result.Unidade);
            Assert.NotEmpty(result.Ibge);
            Assert.NotEmpty(result.Gia);
            Assert.NotEmpty(result.Uf);
        }
    }
}
