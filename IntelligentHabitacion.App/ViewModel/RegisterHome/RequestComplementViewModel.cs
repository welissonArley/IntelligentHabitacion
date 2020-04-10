using IntelligentHabitacion.App.Model;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.RegisterHome
{
    public class RequestComplementViewModel : BaseViewModel
    {
        public ICommand NextCommand { protected set; get; }

        public HomeModel Model { get; set; }

        public RequestComplementViewModel()
        {
            NextCommand = new Command(async () => await OnNext());
        }

        private async Task OnNext()
        {
            try
            {
                await Navigation.PushAsync<RequestNeighborhoodViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
