using IntelligentHabitacion.Model;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.ViewModel.RegisterHome
{
    public class RequestComplementViewModel : BaseViewModel
    {
        public ICommand NextCommand { protected set; get; }

        public RegisterHomeModel Model { get; set; }

        public RequestComplementViewModel()
        {
            NextCommand = new Command(OnNext);
        }

        private void OnNext()
        {
            try
            {
                Navigation.PushAsync<RequestNeighborhoodViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                Exception(exeption);
            }
        }
    }
}
