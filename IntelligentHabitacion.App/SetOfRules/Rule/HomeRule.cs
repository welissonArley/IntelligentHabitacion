using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.SQLite.Interface;
using IntelligentHabitacion.Communication;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Useful;
using IntelligentHabitacion.Validators.Validator;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Rule
{
    public class HomeRule : IHomeRule
    {
        private readonly IIntelligentHabitacionHttpClient _httpClient;
        private readonly ISqliteDatabase _database;

        public HomeRule(IIntelligentHabitacionHttpClient intelligentHabitacionHttpClient, ISqliteDatabase database)
        {
            _httpClient = intelligentHabitacionHttpClient;
            _database = database;
        }

        public async Task Create(RegisterHomeModel Model)
        {
            ValidadeAdress(Model.Address);
            ValidadeCity(Model.City.Name);
            ValidadeNeighborhood(Model.Neighborhood);
            ValidadeNetWorkInformation(Model.NetWork.Name, Model.NetWork.Password);
            ValidadeNumber(Model.Number);

            var response = await _httpClient.CreateHome(new RequestRegisterHomeJson
            {
                Address = Model.Address,
                City = new RequestRegisterCityJson
                {
                    Name = Model.City.Name,
                    State = new RequestRegisterStateJson
                    {
                        Name = Model.City.State.Name,
                        Country = new RequestRegisterCountryJson
                        {
                            Name = Model.City.State.Country.Name,
                            Abbreviation = Model.City.State.Country.Abbreviation
                        }
                    }
                },
                Complement = Model.Complement,
                ZipCode = Model.ZipCode,
                Neighborhood = Model.Neighborhood,
                NetworksName = Model.NetWork.Name,
                NetworksPassword = Model.NetWork.Password,
                Number = Model.Number
            }, _database.Get().Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _database.UpdateToken(response.Token);
            _database.IsAdministrator();
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
