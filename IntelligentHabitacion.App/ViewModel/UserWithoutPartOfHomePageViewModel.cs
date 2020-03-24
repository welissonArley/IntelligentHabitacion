using IntelligentHabitacion.App.ViewModel.RegisterHome;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel
{
    public class UserWithoutPartOfHomePageViewModel : BaseViewModel
    {
        public ICommand CardCreateHomeTapped { get; }
        public ICommand CardMyInformationTapped { get; }

        public UserWithoutPartOfHomePageViewModel()
        {
            CardCreateHomeTapped = new Command(async () => await ClickOnCardCreateHome());
            CardMyInformationTapped = new Command(async () => await ClickOnCardMyInformations());
        }

        private async Task ClickOnCardCreateHome()
        {
            try
            {
                await Navigation.PushAsync<RequestZipCodeViewModel>((viewModel, page) => viewModel.Model = new Model.RegisterHomeModel());
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private async Task ClickOnCardMyInformations()
        {
            try
            {
                await ShowLoading();
                await Navigation.PushAsync<UpdateUserInformationViewModel>();
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
