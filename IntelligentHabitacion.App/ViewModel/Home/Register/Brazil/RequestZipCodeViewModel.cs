using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.Useful;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.Home.Register.Brazil
{
    public class RequestZipCodeViewModel : BaseViewModel
    {
        private readonly IHomeBrazilRule _homeRule;

        public ICommand NextCommand { protected set; get; }

        public HomeModel Model { get; set; }
        public CountryModel Country { get; set; }

        public RequestZipCodeViewModel(IHomeBrazilRule homeRule)
        {
            _homeRule = homeRule;
            NextCommand = new Command(async () => await OnNext());
            Model = new HomeModel();
        }

        private async Task OnNext()
        {
            try
            {
                await ShowLoading();
                var result = await _homeRule.GetLocationByZipCode(Model.ZipCode);
                Model.Neighborhood = result.Neighborhood;
                Model.Address = result.Address;
                Model.City.Name = result.City.Name;
                Model.City.StateProvinceName = result.City.StateProvinceName;
                Model.City.Country = Country;
                HideLoading();
                await Navigation.PushAsync<RegisterHomeViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
    }
}
