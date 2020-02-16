using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception.ErrorJson;
using IntelligentHabitacion.Exception.ExceptionsBase;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Communication
{
    public class IntelligentHabitacionHttpClient : HttpClient
    {
        private async Task<HttpResponseMessage> SendRequisition(HttpMethod httpMethod, string uri)
        {
            HttpRequestMessage request = new HttpRequestMessage(httpMethod, uri);
            var resposta = await SendAsync(request);
            await ResponseValidate(resposta);
            return resposta;
        }
        private async Task ResponseValidate(HttpResponseMessage responseMessage)
        {
            try
            {
                var errorJson = (JsonConvert.DeserializeObject<ErrorTrueJson>(await responseMessage.Content.ReadAsStringAsync()));
                if (errorJson.Erro)
                    throw new RequestException();
            }
            catch (RequestException e)
            {
                throw e;
            }
            catch { }
        }

        public async Task<ResponseLocationBrazilJson> GetLocationBrazilByZipCode(string zipcode)
        {
            var resposta = await SendRequisition(HttpMethod.Get, $"https://viacep.com.br/ws/{zipcode.Replace(".", "").Replace("-","")}/json/");
            return JsonConvert.DeserializeObject<ResponseLocationBrazilJson>(await resposta.Content.ReadAsStringAsync());
        }
    }
}
