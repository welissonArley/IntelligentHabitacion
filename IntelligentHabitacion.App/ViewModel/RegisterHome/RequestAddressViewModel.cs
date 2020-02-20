using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.RegisterHome
{
    public class RequestAddressViewModel : BaseViewModel
    {
        private readonly IHomeRule _homeRule;

        public ICommand NextCommand { protected set; get; }

        public RegisterHomeModel Model { get; set; }

        public RequestAddressViewModel(IHomeRule homeRule)
        {
            _homeRule = homeRule;
            NextCommand = new Command(OnNext);
        }

        private void OnNext()
        {
            try
            {
                _homeRule.ValidadeAdress(Model.Address);
                Navigation.PushAsync<RequestNumberViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                Exception(exeption);
            }
        }
    }
}
