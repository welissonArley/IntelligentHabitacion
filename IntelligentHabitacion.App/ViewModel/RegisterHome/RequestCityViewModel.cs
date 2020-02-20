using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.RegisterHome
{
    public class RequestCityViewModel : BaseViewModel
    {
        private readonly IHomeRule _homeRule;

        public ICommand NextCommand { protected set; get; }

        public RegisterHomeModel Model { get; set; }

        public RequestCityViewModel(IHomeRule homeRule)
        {
            _homeRule = homeRule;
            NextCommand = new Command(OnNext);
        }

        private void OnNext()
        {
            try
            {
                _homeRule.ValidadeCity(Model.City.Name);
                Navigation.PushAsync<RequestAddressViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                Exception(exeption);
            }
        }
    }
}
