using IntelligentHabitacion.Model;
using IntelligentHabitacion.SetOfRules.Interface;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.ViewModel.RegisterHome
{
    public class RequestNetworkInformationViewModel : BaseViewModel
    {
        private readonly IHomeRule _homeRule;

        public ICommand ConcludeCommand { protected set; get; }

        public RegisterHomeModel Model { get; set; }

        public RequestNetworkInformationViewModel(IHomeRule homeRule)
        {
            _homeRule = homeRule;
            ConcludeCommand = new Command(OnConclude);
        }

        private void OnConclude()
        {
            try
            {
                _homeRule.ValidadeNetWorkInformation(Model.NetWork.Name, Model.NetWork.Password);
                Navigation.PopToRootAsync();
            }
            catch (System.Exception exeption)
            {
                Exception(exeption);
            }
        }
    }
}
