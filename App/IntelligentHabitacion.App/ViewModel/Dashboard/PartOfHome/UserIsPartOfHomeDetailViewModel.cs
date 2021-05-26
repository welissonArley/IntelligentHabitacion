using IntelligentHabitacion.App.View.CleaningSchedule;
using IntelligentHabitacion.App.View.Friends;
using IntelligentHabitacion.App.View.Home.Informations;
using IntelligentHabitacion.App.View.MyFoods;
using IntelligentHabitacion.App.View.User.Update;
using IntelligentHabitacion.App.ViewModel.CleaningSchedule;
using IntelligentHabitacion.App.ViewModel.Friends;
using IntelligentHabitacion.App.ViewModel.Home.Informations;
using IntelligentHabitacion.App.ViewModel.MyFoods;
using IntelligentHabitacion.App.ViewModel.User.Update;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

namespace IntelligentHabitacion.App.ViewModel.Dashboard.PartOfHome
{
    public class UserIsPartOfHomeDetailViewModel
    {
        private readonly INavigation Navigation;

        public ICommand CardMyInformationTapped { get; }
        public ICommand CardHomesInformationsTapped { get; }
        public ICommand CardMyFriendsTapped { get; }
        public ICommand CardMyFoodsTapped { get; }
        public ICommand CardCleanHouseTapped { get; }

        public UserIsPartOfHomeDetailViewModel(INavigation navigation)
        {
            Navigation = navigation;
            
            CardMyInformationTapped = new Command(async () => await ClickOnCardMyInformations());
            CardHomesInformationsTapped = new Command(async () => await ClickOnCardHomesInformations());
            CardMyFriendsTapped = new Command(async () => await ClickOnCardMyFriends());
            CardMyFoodsTapped = new Command(async () => await ClickOnCardMyFoods());
            CardCleanHouseTapped = new Command(async () => await ClickOnCardCleanHouse());
        }
        
        private async Task ClickOnCardMyInformations()
        {
            var page = (Page)ViewFactory.CreatePage<UserInformationViewModel, UserInformationPage>(async (viewModel, _) =>
            {
                await viewModel.Initialize();
            });

            await Navigation.PushAsync(page);
        }
        private async Task ClickOnCardHomesInformations()
        {
            var page = (Page)ViewFactory.CreatePage<HomeInformationViewModel, HomeInformationPage>(async (viewModel, _) =>
            {
                await viewModel.Initialize();
            });

            await Navigation.PushAsync(page);
        }
        private async Task ClickOnCardMyFriends()
        {
            var page = (Page)ViewFactory.CreatePage<MyFriendsViewModel, MyFriendsPage>(async (viewModel, _) =>
            {
                await viewModel.Initialize();
            });

            await Navigation.PushAsync(page);
        }
        private async Task ClickOnCardMyFoods()
        {
            var page = (Page)ViewFactory.CreatePage<MyFoodsViewModel, MyFoodsPage>(async (viewModel, _) =>
            {
                await viewModel.Initialize();
            });

            await Navigation.PushAsync(page);
        }
        private async Task ClickOnCardCleanHouse()
        {
            var page = (Page)ViewFactory.CreatePage<TasksViewModel, TasksPage>(async (viewModel, _) =>
            {
                await viewModel.Initialize();
            });

            await Navigation.PushAsync(page);
        }
    }
}
