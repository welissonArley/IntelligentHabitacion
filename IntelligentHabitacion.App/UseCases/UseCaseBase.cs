using IntelligentHabitacion.App.Services.Communication;
using System.Linq;
using System.Net.Http.Headers;

namespace IntelligentHabitacion.App.UseCases
{
    public class UseCaseBase
    {
        private readonly StringWithQualityHeaderValue _language = new StringWithQualityHeaderValue(System.Globalization.CultureInfo.CurrentCulture.ToString());
        private readonly string _baseAddres;

        public UseCaseBase(string controller)
        {
            _baseAddres = $"https://{RestEndPoints.BaseUrl}/api/v1/{controller}";
        }

        protected StringWithQualityHeaderValue GetLanguage()
        {
            return _language;
        }

        protected string BaseAddress()
        {
            return _baseAddres;
        }

        protected string GetTokenOnHeaderRequest(HttpResponseHeaders headers)
        {
            return headers.Contains("Tvih") ? headers.GetValues("Tvih")?.First() : null;
        }
    }
}
