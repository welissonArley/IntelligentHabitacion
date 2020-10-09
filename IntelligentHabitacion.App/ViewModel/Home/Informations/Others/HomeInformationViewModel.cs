using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
using Plugin.Clipboard;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.Home.Informations.Others
{
    public class HomeInformationViewModel : HomeInformationBaseViewModel
    {
        private readonly IHomeOthersCountryRule _homeRule;

        public HomeInformationViewModel(IHomeOthersCountryRule homeRule, UserPreferences userPreferences)
        {
            IsAdministrator = userPreferences.IsAdministrator;
            _homeRule = homeRule;
            UpdateInformationsTapped = new Command(async () => await ClickUpdateInformations());
        }

        protected override async Task ClickUpdateInformations()
        {
            try
            {
                await ShowLoading();
                await _homeRule.UpdateInformations(Model);
                HideLoading();
                await Navigation.PopAsync();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
    }
}
