using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.Communication;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Useful;
using IntelligentHabitacion.Validators.Validator;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Rule
{
    public class HomeRule : IHomeRule
    {
        private readonly IntelligentHabitacionHttpClient _httpClient;

        public HomeRule(IntelligentHabitacionHttpClient intelligentHabitacionHttpClient)
        {
            _httpClient = intelligentHabitacionHttpClient;
        }

        public void ValidadeAdress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new AddressEmptyException();
        }

        public void ValidadeCity(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                throw new CityEmptyException();
        }

        public void ValidadeNeighborhood(string neighborhood)
        {
            if (string.IsNullOrWhiteSpace(neighborhood))
                throw new NeighborhoodEmptyException();
        }

        public void ValidadeNetWorkInformation(string name, string password)
        {
            if ((string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(password)) || (!string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(password)))
                throw new NetworkInformationsInvalidException();
        }

        public void ValidadeNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new NumberEmptyException();
        }

        public async Task<ResponseLocationJson> ValidadeZipCode(string zipCode)
        {
            new ZipCodeValidator().IsValid(zipCode);

            var result = await _httpClient.GetLocationBrazilByZipCode(zipCode);

            return new ResponseLocationJson
            {
                City = result.Localidade,
                Street = result.Logradouro,
                Neighborhood = result.Bairro,
                State = new ResponseStateJson
                {
                    Abbreviation = result.Uf,
                    Name = new State().StateAbbreviationToFullNameState(result.Uf),
                    Country = new ResponseCountryJson
                    {
                        Abbreviation = "BR",
                        Name = "Brasil"
                    }
                }
            };
        }
    }
}
