using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.SetOfRules.Rule.Operations.RegisterHome;
using IntelligentHabitacion.Communication;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Rule
{
    public class HomeOthersCountryRule : HomeRule, IHomeOthersCountryRule
    {
        private readonly IIntelligentHabitacionHttpClient _httpClient;
        private readonly UserPreferences _userPreferences;

        public HomeOthersCountryRule(IIntelligentHabitacionHttpClient intelligentHabitacionHttpClient, UserPreferences userPreferences)
            : base(intelligentHabitacionHttpClient, userPreferences)
        {
            _httpClient = intelligentHabitacionHttpClient;
            _userPreferences = userPreferences;
        }

        public async override Task Create(HomeModel model)
        {
            var requestHomeJson = new OthersHomeRegisterStrategy().CreateRequest(model);

            var response = await _httpClient.CreateHome(requestHomeJson, _userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());
            _userPreferences.ChangeToken(response.Token);
            _userPreferences.UserIsAdministrator(true);
        }

        public async override Task UpdateInformations(HomeModel model)
        {
            var requestHomeJson = new OthersHomeRegisterStrategy().CreateRequest(model);

            var response = await _httpClient.UpdateHome(requestHomeJson, _userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);
        }
    }
}
