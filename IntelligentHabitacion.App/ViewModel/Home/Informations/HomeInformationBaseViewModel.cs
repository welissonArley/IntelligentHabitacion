using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.ViewModel.Home.Delete;
using Plugin.Clipboard;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.Home.Informations
{
    public abstract class HomeInformationBaseViewModel : BaseViewModel
    {
        public bool IsAdministrator { get; set; }

        public HomeModel Model { get; set; }

        public ICommand DeleteHomeTapped { get; }
        public ICommand CopyWifiPasswordTapped { get; }
        public ICommand UpdateInformationsTapped { get; protected set; }

        public HomeInformationBaseViewModel()
        {
            DeleteHomeTapped = new Command(async () => await ClickDeleteHome());
            CopyWifiPasswordTapped = new Command(async () => await ClickCopyWifiPassword());
        }

        protected async Task ClickDeleteHome()
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

        protected async Task ClickCopyWifiPassword()
        {
            if (!string.IsNullOrWhiteSpace(Model.NetWork.Password))
            {
                CrossClipboard.Current.SetText(Model.NetWork.Password);
                await ShowQuickInformation(ResourceText.INFORMATION_PASSWORD_COPIED_SUCCESSFULLY);
            }
        }

        protected abstract Task ClickUpdateInformations();
    }
}
