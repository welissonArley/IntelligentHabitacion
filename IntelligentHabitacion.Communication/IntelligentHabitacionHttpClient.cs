using IntelligentHabitacion.Communication.Boolean;
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
        private const string UrlIntelligentHabitacionApi = "https://33fbc726.ngrok.io/api/v1";

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
                            throw new IntelligentHabitacionException(ResourceTextException.UNKNOW_ERROR);
                        }
                }
            }
        }

        public async Task<ResponseLocationBrazilJson> GetLocationBrazilByZipCode(string zipcode)
        {
            var response = await SendRequisition(HttpMethod.Get, $"https://viacep.com.br/ws/{zipcode.Replace(".", "").Replace("-","")}/json/");

            var errorJson = (JsonConvert.DeserializeObject<ErrorTrueJson>(await response.Content.ReadAsStringAsync()));
            if (errorJson.Erro)
                throw new ZipCodeInvalidException();

            return JsonConvert.DeserializeObject<ResponseLocationBrazilJson>(await response.Content.ReadAsStringAsync());
        }

        public async Task CreateUser(RequestRegisterUserJson registerUser, string language = null)
        {
            await SendRequisition(HttpMethod.Post, $"{UrlIntelligentHabitacionApi}/User", registerUser, language: language);
        }
        public async Task<BooleanJson> EmailAlreadyBeenRegistered(string email, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Get, $"{UrlIntelligentHabitacionApi}/User/EmailAlreadyBeenRegistered/{email}", language: language);

            return JsonConvert.DeserializeObject<BooleanJson>(await response.Content.ReadAsStringAsync());
        }
    }
}
