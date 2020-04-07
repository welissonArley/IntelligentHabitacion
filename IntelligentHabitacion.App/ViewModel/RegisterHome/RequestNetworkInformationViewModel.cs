using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.View;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

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
                await ShowLoading();

                _homeRule.ValidadeNetWorkInformation(Model.NetWork.Name, Model.NetWork.Password);

                await _homeRule.Create(Model);

                Application.Current.MainPage = new NavigationPage((Page)ViewFactory.CreatePage<UserIsPartOfHomeViewModel, UserIsPartOfHomePage>());

                HideLoading();

                await Navigation.PopToRootAsync();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
    }
}
