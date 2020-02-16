using IntelligentHabitacion.Model;
using IntelligentHabitacion.SetOfRules.Interface;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.ViewModel.RegisterHome
{
    public class RequestNumberViewModel : BaseViewModel
    {
        private readonly IHomeRule _homeRule;

        public ICommand NextCommand { protected set; get; }

        public RegisterHomeModel Model { get; set; }

        public RequestNumberViewModel(IHomeRule homeRule)
        {
            _homeRule = homeRule;
            NextCommand = new Command(OnNext);
        }

        private void OnNext()
        {
            try
            {
                _homeRule.ValidadeNumber(Model.Number);
                Navigation.PushAsync<RequestComplementViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                Exception(exeption);
            }
        }
    }
}
