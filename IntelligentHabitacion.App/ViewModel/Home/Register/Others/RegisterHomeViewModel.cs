using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.View.Login;
using IntelligentHabitacion.App.ViewModel.Login;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

namespace IntelligentHabitacion.App.ViewModel.Home.Register.Others
{
    public class RegisterHomeViewModel : BaseViewModel
    {
        private readonly IHomeOthersCountryRule _homeRule;

        public HomeModel Model { get; set; }

        public ICommand OnConcludeCommand { protected set; get; }

        public RegisterHomeViewModel(IHomeOthersCountryRule homeRule)
        {
            OnConcludeCommand = new Command(async () => await OnConclude());
            _homeRule = homeRule;
        }

        private async Task OnConclude()
        {
            try
            {
                await ShowLoading();

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
