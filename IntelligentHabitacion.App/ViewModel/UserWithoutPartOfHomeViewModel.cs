using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.ViewModel.RegisterHome;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel
{
    public class UserWithoutPartOfHomeViewModel : BaseViewModel
    {
        public ICommand CardCreateHomeTapped { get; }
        public ICommand CardMyInformationTapped { get; }
        public ICommand CardJoinHomeTapped { get; }

        public UserWithoutPartOfHomeViewModel()
        {
            CardCreateHomeTapped = new Command(async () => await ClickOnCardCreateHome());
            CardMyInformationTapped = new Command(async () => await ClickOnCardMyInformations());
            CardJoinHomeTapped = new Command(async () => await ClickOnCardJoinHome());
        }

        private async Task ClickOnCardCreateHome()
        {
            try
            {
                await Navigation.PushAsync<RequestZipCodeViewModel>((viewModel, page) => viewModel.Model = new Model.HomeModel());
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
        private async Task ClickOnCardJoinHome()
        {
            var scanner = new QrCodeService();
            var result = await scanner.Scan();
        }
    }
}
