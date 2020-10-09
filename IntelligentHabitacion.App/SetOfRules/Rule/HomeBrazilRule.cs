using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.SetOfRules.Rule.Operations.RegisterHome;
using IntelligentHabitacion.Communication;
using IntelligentHabitacion.Useful;
using IntelligentHabitacion.Validators.Validator;
using System.Linq;
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
            var requestHomeJson = new BrazilHomeRegisterStrategy().CreateRequestHomeJson(model);

            var response = await _httpClient.CreateHome(requestHomeJson, _userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());
            _userPreferences.ChangeToken(response.Token);
            _userPreferences.UserIsAdministrator(true);
        }

        public async override Task UpdateInformations(HomeModel model)
        {
            var requestHomeJson = new BrazilHomeRegisterStrategy().CreateRequestHomeJson(model);

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
                    StateProvinceName = new State().StateAbbreviationToFullNameState(result.Uf)
                },
                Address = result.Logradouro,
                Neighborhood = result.Bairro,
            };
        }
    }
}
