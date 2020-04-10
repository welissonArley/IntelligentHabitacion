using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel
{
    public class UserIsPartOfHomeViewModel : BaseViewModel
    {
        public ICommand CardMyInformationTapped { get; }
        public ICommand CardHomesInformationsTapped { get; }

        public UserIsPartOfHomeViewModel()
        {
            CardMyInformationTapped = new Command(async () => await ClickOnCardMyInformations());
            CardHomesInformationsTapped = new Command(async () => await ClickOnCardHomesInformations());
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

        private async Task ClickOnCardHomesInformations()
        {
            try
            {
                await ShowLoading();
                await Navigation.PushAsync<HomeInformationViewModel>();
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
