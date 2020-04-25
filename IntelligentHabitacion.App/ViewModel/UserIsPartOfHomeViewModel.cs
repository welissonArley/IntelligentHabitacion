using IntelligentHabitacion.App.ViewModel.Friends;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel
{
    public class UserIsPartOfHomeViewModel : BaseViewModel
    {
        public ICommand CardMyInformationTapped { get; }
        public ICommand CardHomesInformationsTapped { get; }
        public ICommand CardMyFriendsTapped { get; }

        public UserIsPartOfHomeViewModel()
        {
            CardMyInformationTapped = new Command(async () => await ClickOnCardMyInformations());
            CardHomesInformationsTapped = new Command(async () => await ClickOnCardHomesInformations());
            CardMyFriendsTapped = new Command(async () => await ClickOnCardMyFriends());
        }

        private async Task ClickOnCardMyInformations()
        {
            try
            {
                await ShowLoading();
                await Navigation.PushAsync<UpdateUserInformationViewModel>();
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
                await Navigation.PushAsync<HomeInformationViewModel>();
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
    }
}
