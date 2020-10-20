using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.View.Login;
using IntelligentHabitacion.App.View.Modal;
using IntelligentHabitacion.App.ViewModel.Login;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel.Home.Register.Others
{
    public class RegisterHomeViewModel : BaseViewModel
    {
        private readonly IHomeOthersCountryRule _homeRule;

        public HomeModel Model { get; set; }

        public ICommand OnConcludeCommand { protected set; get; }
        public ICommand WhyINeedFillThisInformationCommand { protected set; get; }

        public RegisterHomeViewModel(IHomeOthersCountryRule homeRule)
        {
            OnConcludeCommand = new Command(async () => await OnConclude());
            WhyINeedFillThisInformationCommand = new Command(async () =>
            {
                var navigation = Resolver.Resolve<INavigation>();
                await navigation.PushPopupAsync(new LgpdModal(ResourceText.DESCRIPTION_WHY_WE_NEED_YOUR_ADDRESS));
            });
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
