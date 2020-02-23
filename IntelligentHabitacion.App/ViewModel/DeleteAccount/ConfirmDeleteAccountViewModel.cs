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
            CancelCommand = new Command(OnCancel);
            ConfirmCommand = new Command(OnConfirm);
        }

        private void OnCancel()
        {
            try
            {
                Navigation.PopAsync();
            }
            catch (System.Exception exeption)
            {
                Exception(exeption);
            }
        }

        private void OnConfirm()
        {
            try
            {
                Navigation.PushAsync<DeleteAccountViewModel>();
            }
            catch (System.Exception exeption)
            {
                Exception(exeption);
            }
        }
    }
}
