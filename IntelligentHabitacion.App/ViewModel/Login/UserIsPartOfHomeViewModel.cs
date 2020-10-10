using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.ViewModel.Friends;
using IntelligentHabitacion.App.ViewModel.MyFoods;
using IntelligentHabitacion.App.ViewModel.User.Update;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel.Login
{
    public class UserIsPartOfHomeViewModel : BaseViewModel
    {
        public ICommand CardMyInformationTapped { get; }
        public ICommand CardHomesInformationsTapped { get; }
        public ICommand CardMyFriendsTapped { get; }
        public ICommand CardMyFoodsTapped { get; }

        public UserIsPartOfHomeViewModel()
        {
            CardMyInformationTapped = new Command(async () => await ClickOnCardMyInformations());
            CardHomesInformationsTapped = new Command(async () => await ClickOnCardHomesInformations());
            CardMyFriendsTapped = new Command(async () => await ClickOnCardMyFriends());
            CardMyFoodsTapped = new Command(async () => await ClickOnCardMyFoods());
        }

        private async Task ClickOnCardMyInformations()
        {
            try
            {
                await ShowLoading();
                await Navigation.PushAsync<UserInformationViewModel>();
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
        private async Task ClickOnCardHomesInformations()
        {
            try
            {
                await ShowLoading();

                /*
                 Which business rule will call GetInformations doesn’t matter,
                as its implementation doesn’t depend on the country, the API manages this for us.
                 */
                var homeRule = Resolver.Resolve<IHomeBrazilRule>();
                var home = await homeRule.GetInformations();

                if (home.IsBrazil())
                {
                    await Navigation.PushAsync<Home.Informations.Brazil.HomeInformationViewModel>((viewModel, page) =>
                    {
                        viewModel.Model = home;
                        viewModel._currentZipCode = home.ZipCode;
                    });
                }
                else
                    await Navigation.PushAsync<Home.Informations.Others.HomeInformationViewModel>((viewModel, page) => viewModel.Model = home);

                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
        private async Task ClickOnCardMyFriends()
        {
            try
            {
                await ShowLoading();
                await Navigation.PushAsync<MyFriendsViewModel>();
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
        private async Task ClickOnCardMyFoods()
        {
            try
            {
                await ShowLoading();
                await Navigation.PushAsync<MyFoodsViewModel>();
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
    }
}
