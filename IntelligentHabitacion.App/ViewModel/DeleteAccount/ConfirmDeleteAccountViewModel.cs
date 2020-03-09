using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.DeleteAccount
{
    public class ConfirmDeleteAccountViewModel : BaseViewModel
    {
        public ICommand CancelCommand { protected set; get; }
        public ICommand ConfirmCommand { protected set; get; }

        public ConfirmDeleteAccountViewModel()
        {
            CancelCommand = new Command(async () => await OnCancel());
            ConfirmCommand = new Command(async () => await OnConfirm());
        }

        private async Task OnCancel()
        {
            try
            {
                await Navigation.PopAsync();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        private async Task OnConfirm()
        {
            try
            {
                await Navigation.PushAsync<DeleteAccountViewModel>();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
