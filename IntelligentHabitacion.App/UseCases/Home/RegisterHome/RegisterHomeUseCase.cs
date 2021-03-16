using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.Services.Communication.Home;
using IntelligentHabitacion.App.UseCases.Home.RegisterHome.Strategy;
using Refit;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.Home.RegisterHome
{
    public class RegisterHomeUseCase : UseCaseBase, IRegisterHomeUseCase
    {
        private readonly UserPreferences _userPreferences;
        private readonly IHomeService _restService;
        private readonly ContextStrategy _contextStrategy;

        public RegisterHomeUseCase(UserPreferences userPreferences) : base("Home")
        {
            _userPreferences = userPreferences;
            _restService = RestService.For<IHomeService>(BaseAddress());
            _contextStrategy = new ContextStrategy();
        }

        public async Task Execute(HomeModel home)
        {
            var strategy = _contextStrategy.GetStrategy(home.City.Country);

            strategy.Validate(home);

            var request = strategy.Mapper(home);

            var response = await _restService.CreateHome(request, await _userPreferences.GetToken(), GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));

            _userPreferences.UserIsAdministrator(true);
        }
    }
}
