using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.SetOfRules.Rule.Operations.RegisterHome;
using IntelligentHabitacion.App.Useful.Validator;
using IntelligentHabitacion.Communication;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Rule
{
    public class HomeBrazilRule : HomeRule, IHomeBrazilRule
    {
        private readonly IIntelligentHabitacionHttpClient _httpClient;
        private readonly UserPreferences _userPreferences;

        public HomeBrazilRule(IIntelligentHabitacionHttpClient intelligentHabitacionHttpClient, UserPreferences userPreferences)
            : base(intelligentHabitacionHttpClient, userPreferences)
        {
            _httpClient = intelligentHabitacionHttpClient;
            _userPreferences = userPreferences;
        }

        public async override Task Create(HomeModel model)
        {
            var requestHomeJson = new BrazilHomeRegisterStrategy().CreateRequestToRegisterHome(model);

            var response = await _httpClient.CreateHome(requestHomeJson, _userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());
            _userPreferences.ChangeToken(response.Token);
            _userPreferences.UserIsAdministrator(true);
        }

        public async override Task UpdateInformations(HomeModel model)
        {
            var requestHomeJson = new BrazilHomeRegisterStrategy().CreateRequestToUpdateHome(model);

            var response = await _httpClient.UpdateHome(requestHomeJson, _userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);
        }

        public async Task<HomeModel> GetLocationByZipCode(string zipCode)
        {
            new ZipCodeValidator().IsValid(zipCode);

            var result = await _httpClient.GetLocationBrazilByZipCode(zipCode);

            return new HomeModel
            {
                City = new CityModel
                {
                    Name = result.Localidade,
                    StateProvinceName = StateAbbreviationToFullNameState(result.Uf)
                },
                Address = result.Logradouro,
                Neighborhood = result.Bairro,
            };
        }

        private string StateAbbreviationToFullNameState(string abbreviation)
        {
            switch (abbreviation.ToUpper())
            {
                case "AC":
                    {
                        return "Acre";
                    }
                case "AL":
                    {
                        return "Alagoas";
                    }
                case "AP":
                    {
                        return "Amapá";
                    }
                case "AM":
                    {
                        return "Amazonas";
                    }
                case "BA":
                    {
                        return "Bahia";
                    }
                case "CE":
                    {
                        return "Ceará";
                    }
                case "ES":
                    {
                        return "Espírito Santo";
                    }
                case "GO":
                    {
                        return "Goiás";
                    }
                case "MA":
                    {
                        return "Maranhão";
                    }
                case "MT":
                    {
                        return "Mato Grosso";
                    }
                case "MS":
                    {
                        return "Mato Grosso do Sul";
                    }
                case "MG":
                    {
                        return "Minas Gerais";
                    }
                case "PA":
                    {
                        return "Pará";
                    }
                case "PB":
                    {
                        return "Paraíba";
                    }
                case "PR":
                    {
                        return "Paraná";
                    }
                case "PE":
                    {
                        return "Pernambuco";
                    }
                case "PI":
                    {
                        return "Piauí";
                    }
                case "RJ":
                    {
                        return "Rio de Janeiro";
                    }
                case "RN":
                    {
                        return "Rio Grande do Norte";
                    }
                case "RS":
                    {
                        return "Rio Grande do Sul";
                    }
                case "RO":
                    {
                        return "Rondônia";
                    }
                case "RR":
                    {
                        return "Roraima";
                    }
                case "SC":
                    {
                        return "Santa Catarina";
                    }
                case "SP":
                    {
                        return "São Paulo";
                    }
                case "SE":
                    {
                        return "Sergipe";
                    }
                case "TO":
                    {
                        return "Tocantins";
                    }
                case "DF":
                    {
                        return "Distrito Federal";
                    }
                default:
                    {
                        return "";
                    }
            }
        }
    }
}
