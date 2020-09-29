using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
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
        private readonly UserPreferences _userPreferences;

        public HomeRule(IIntelligentHabitacionHttpClient intelligentHabitacionHttpClient, UserPreferences userPreferences)
        {
            _httpClient = intelligentHabitacionHttpClient;
            _userPreferences = userPreferences;
        }

        public async Task Create(HomeModel model)
        {
            ValidadeAdress(model.Address);
            ValidadeCity(model.City.Name);
            ValidadeNeighborhood(model.Neighborhood);
            ValidadeNetWorkInformation(model.NetWork.Name, model.NetWork.Password);
            ValidadeNumber(model.Number);
            ValidadeDeadlinePaymentRent(model.DeadlinePaymentRent);

            var response = await _httpClient.CreateHome(CreateRequestHomeJson(model), _userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);
            _userPreferences.UserIsAdministrator(true);
        }

        public async Task Delete(string code, string password)
        {
            var response = await _httpClient.DeleteHome(new RequestAdminActionJson
            {
                Code = code,
                Password = password
            }, _userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);
        }

        public async Task<HomeModel> GetInformations()
        {
            var response = await _httpClient.GetHomesInformations(_userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);

            var homeInformations = (ResponseHomeInformationsJson)response.Response;

            return new HomeModel
            {
                Address = homeInformations.Address,
                Complement = homeInformations.Complement,
                Neighborhood = homeInformations.Neighborhood,
                Number = homeInformations.Number,
                ZipCode = homeInformations.ZipCode,
                DeadlinePaymentRent = homeInformations.DeadlinePaymentRent,
                NetWork = new WifiNetworkModel
                {
                    Name = homeInformations.NetWork.Name,
                    Password = homeInformations.NetWork.Password
                },
                City = new CityModel
                {
                    Name = homeInformations.City,
                    State = new StateModel
                    {
                        Name = homeInformations.State.Name,
                        Country = new CountryModel
                        {
                            Abbreviation = homeInformations.State.Country.Abbreviation,
                            Name = homeInformations.State.Country.Name
                        }
                    }
                }
            };
        }

        public async Task RequestCodeDeleteHome()
        {
            var response = await _httpClient.RequestCodeToDeleteHome(_userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);
        }

        public async Task UpdateInformations(HomeModel model)
        {
            ValidadeAdress(model.Address);
            ValidadeCity(model.City.Name);
            ValidadeNeighborhood(model.Neighborhood);
            ValidadeNetWorkInformation(model.NetWork.Name, model.NetWork.Password);
            ValidadeNumber(model.Number);
            ValidadeDeadlinePaymentRent(model.DeadlinePaymentRent);

            var response = await _httpClient.UpdateHome(CreateRequestHomeJson(model), _userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);
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

        public void ValidadeDeadlinePaymentRent(short? deadline)
        {
            if(!deadline.HasValue || !(deadline >= 1 && deadline <= 28))
                throw new DeadlinePaymentRentException();
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

        private RequestHomeJson CreateRequestHomeJson(HomeModel model)
        {
            return new RequestHomeJson
            {
                Address = model.Address,
                City = new RequestRegisterCityJson
                {
                    Name = model.City.Name,
                    State = new RequestRegisterStateJson
                    {
                        Name = model.City.State.Name,
                        Country = new RequestRegisterCountryJson
                        {
                            Name = model.City.State.Country.Name,
                            Abbreviation = model.City.State.Country.Abbreviation
                        }
                    }
                },
                Complement = model.Complement,
                ZipCode = model.ZipCode,
                Neighborhood = model.Neighborhood,
                NetworksName = model.NetWork.Name,
                NetworksPassword = model.NetWork.Password,
                Number = model.Number,
                DeadlinePaymentRent = model.DeadlinePaymentRent.Value
            };
        }
    }
}
