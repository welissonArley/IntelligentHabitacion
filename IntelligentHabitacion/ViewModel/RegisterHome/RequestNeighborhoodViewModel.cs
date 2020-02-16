using IntelligentHabitacion.Model;
using IntelligentHabitacion.SetOfRules.Interface;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.ViewModel.RegisterHome
{
    public class RequestNeighborhoodViewModel : BaseViewModel
    {
        private readonly IHomeRule _homeRule;

        public ICommand NextCommand { protected set; get; }

        public RegisterHomeModel Model { get; set; }

        public RequestNeighborhoodViewModel(IHomeRule homeRule)
        {
            _homeRule = homeRule;
            NextCommand = new Command(OnNext);
        }

        private void OnNext()
        {
            try
            {
                _homeRule.ValidadeNeighborhood(Model.Neighborhood);
                Navigation.PushAsync<RequestNetworkInformationViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                Exception(exeption);
            }
        }
    }
}
