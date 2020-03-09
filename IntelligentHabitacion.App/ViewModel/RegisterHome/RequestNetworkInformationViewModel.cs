using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.RegisterHome
{
    public class RequestNetworkInformationViewModel : BaseViewModel
    {
        private readonly IHomeRule _homeRule;

        public ICommand ConcludeCommand { protected set; get; }

        public RegisterHomeModel Model { get; set; }

        public RequestNetworkInformationViewModel(IHomeRule homeRule)
        {
            _homeRule = homeRule;
            ConcludeCommand = new Command(async () => await OnConclude());
        }

        private async Task OnConclude()
        {
            try
            {
                _homeRule.ValidadeNetWorkInformation(Model.NetWork.Name, Model.NetWork.Password);
                await Navigation.PopToRootAsync();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
