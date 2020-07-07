using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.SQLite.Interface;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel
{
    public class HomeInformationViewModel : BaseViewModel
    {
        private string _currentZipCode;
        private readonly IHomeRule _homeRule;
        
        public bool IsAdministrator { get; set; }

        public HomeModel Model { get; set; }

        public System.EventHandler<FocusEventArgs> ZipCodeChangedUnfocused { get; set; }

        public ICommand UpdateInformationsTapped { get; }
        public ICommand DeleteHomeTapped { get; }

        public HomeInformationViewModel(IHomeRule homeRule, ISqliteDatabase database)
        {
            IsAdministrator = database.Get().IsAdministrator;
            _homeRule = homeRule;
            UpdateInformationsTapped = new Command(async () => await ClickUpdateInformations());
            DeleteHomeTapped = new Command(async () => await ClickDeleteHome());
            ZipCodeChangedUnfocused += async (sender, e) =>
            {
                await VerifyZipCode();
            };
            Model = Task.Run(async () => await homeRule.GetInformations()).Result;
            _currentZipCode = Model.ZipCode;
        }

        private async Task ClickUpdateInformations()
        {
            try
            {
                await ShowLoading();
                await _homeRule.UpdateInformations(Model);
                await Navigation.PopAsync();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
        private async Task ClickDeleteHome()
        {
            try
            {
                await ShowLoading();
                await Navigation.PushAsync<DeleteHomeViewModel>();
                HideLoading();
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
                var result = await _homeRule.ValidadeZipCode(Model.ZipCode);
                
                _currentZipCode = Model.ZipCode;
                Model.Neighborhood = result.Neighborhood;
                Model.Address = result.Street;
                Model.City.Name = result.City;
                Model.City.State.Name = result.State.Name;
                Model.City.State.Country.Name = result.State.Country.Name;
                Model.City.State.Country.Abbreviation = result.State.Country.Abbreviation;

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
