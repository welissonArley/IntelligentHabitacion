using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.Home.Informations.Brazil
{
    public class HomeInformationViewModel : HomeInformationBaseViewModel
    {
        public string _currentZipCode;
        private readonly IHomeBrazilRule _homeRule;

        public System.EventHandler<FocusEventArgs> ZipCodeChangedUnfocused { get; set; }

        public HomeInformationViewModel(IHomeBrazilRule homeRule, UserPreferences userPreferences)
        {
            IsAdministrator = userPreferences.IsAdministrator;
            _homeRule = homeRule;
            UpdateInformationsTapped = new Command(async () => await ClickUpdateInformations());
            ZipCodeChangedUnfocused += async (sender, e) =>
            {
                await VerifyZipCode();
            };
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
        
        private async Task VerifyZipCode()
        {
            try
            {
                if (_currentZipCode.Equals(Model.ZipCode))
                    return;

                await ShowLoading();
                if (Application.Current.MainPage.Navigation.NavigationStack.Count == 1)
                {
                    HideLoading();
                    return;
                }
                var result = await _homeRule.GetLocationByZipCode(Model.ZipCode);

                _currentZipCode = Model.ZipCode;
                Model.Neighborhood = result.Neighborhood;
                Model.Address = result.Address;
                Model.City.Name = result.City.Name;
                Model.City.StateProvinceName = result.City.StateProvinceName;

                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
    }
}
