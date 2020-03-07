using IntelligentHabitacion.Communication.Error;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ErrorJson;
using IntelligentHabitacion.Exception.ExceptionsBase;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Communication
{
    public class IntelligentHabitacionHttpClient : HttpClient
    {
        private const string UrlIntelligentHabitacionApi = "https://839eeac8.ngrok.io/api/v1";

        private async Task<HttpResponseMessage> SendRequisition(HttpMethod httpMethod, string uri, object content = null, string language = null)
        {
            HttpRequestMessage request = new HttpRequestMessage(httpMethod, uri);

            if (content != null)
                request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            if (!string.IsNullOrWhiteSpace(language))
                request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(language));
            else
                request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue("pt-BR"));

            var resposta = await SendAsync(request);
            await ResponseValidate(resposta);
            return resposta;
        }
        private async Task ResponseValidate(HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorJson = JsonConvert.DeserializeObject<ErrorJson>(await responseMessage.Content.ReadAsStringAsync());
                switch (responseMessage.StatusCode)
                {
                    case System.Net.HttpStatusCode.BadRequest:
                        {
                            throw new ErrorOnValidationException(errorJson.Errors);
                        }
                    case System.Net.HttpStatusCode.NotFound:
                        {
                            throw new NotFoundException(errorJson.Errors[0]);
                        }
                    case System.Net.HttpStatusCode.Unauthorized:
                        {
                            throw new InvalidLoginException();
                        }
                    default:
                        {
                            throw new System.Exception(ResourceTextException.UNKNOW_ERROR);
                        }
                }
            }
        }

        public async Task<ResponseLocationBrazilJson> GetLocationBrazilByZipCode(string zipcode)
        {
            var resposta = await SendRequisition(HttpMethod.Get, $"https://viacep.com.br/ws/{zipcode.Replace(".", "").Replace("-","")}/json/");

            var errorJson = (JsonConvert.DeserializeObject<ErrorTrueJson>(await resposta.Content.ReadAsStringAsync()));
            if (errorJson.Erro)
                throw new ZipCodeInvalidException();

            return JsonConvert.DeserializeObject<ResponseLocationBrazilJson>(await resposta.Content.ReadAsStringAsync());
        }

        public async Task CreateUser(RequestRegisterUserJson registerUser, string language = null)
        {
            await SendRequisition(HttpMethod.Post, $"{UrlIntelligentHabitacionApi}/User", registerUser, language: language);
        }
    }
}
