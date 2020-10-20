using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.View.Modal;
using IntelligentHabitacion.Useful;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel.Home.Register.Brazil
{
    public class RequestZipCodeViewModel : BaseViewModel
    {
        private readonly IHomeBrazilRule _homeRule;

        public ICommand NextCommand { protected set; get; }
        public ICommand WhyINeedFillThisInformationCommand { protected set; get; }

        public HomeModel Model { get; set; }
        public CountryModel Country { get; set; }

        public RequestZipCodeViewModel(IHomeBrazilRule homeRule)
        {
            _homeRule = homeRule;
            NextCommand = new Command(async () => await OnNext());
            WhyINeedFillThisInformationCommand = new Command(async () =>
            {
                var navigation = Resolver.Resolve<INavigation>();
                await navigation.PushPopupAsync(new LgpdModal(ResourceText.DESCRIPTION_WHY_WE_NEED_YOUR_CEP));
            });
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
