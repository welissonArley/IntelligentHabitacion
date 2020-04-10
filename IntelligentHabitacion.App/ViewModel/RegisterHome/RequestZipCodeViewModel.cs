using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.RegisterHome
{
    public class RequestZipCodeViewModel : BaseViewModel
    {
        private readonly IHomeRule _homeRule;

        public ICommand NextCommand { protected set; get; }

        public HomeModel Model { get; set; }

        public RequestZipCodeViewModel(IHomeRule homeRule)
        {
            _homeRule = homeRule;
            NextCommand = new Command(async () => await OnNext());
        }

        private async Task OnNext()
        {
            try
            {
                await ShowLoading();
                var result = await _homeRule.ValidadeZipCode(Model.ZipCode);
                Model.Neighborhood = result.Neighborhood;
                Model.Address = result.Street;
                Model.City.Name = result.City;
                Model.City.State.Name = result.State.Name;
                Model.City.State.Abbreviation = result.State.Abbreviation;
                Model.City.State.Country.Name = result.State.Country.Name;
                Model.City.State.Country.Abbreviation = result.State.Country.Abbreviation;
                HideLoading();
                await Navigation.PushAsync<RequestCityViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
    }
}
