using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel
{
    public class UserIsPartOfHomeViewModel : BaseViewModel
    {
        public ICommand CardMyInformationTapped { get; }

        public UserIsPartOfHomeViewModel()
        {
            CardMyInformationTapped = new Command(async () => await ClickOnCardMyInformations());
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
