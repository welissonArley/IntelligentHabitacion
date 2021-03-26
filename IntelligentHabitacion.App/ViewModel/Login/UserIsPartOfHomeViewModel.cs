using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.View.Modal.MenuOptions;
using IntelligentHabitacion.App.ViewModel.Friends;
using IntelligentHabitacion.App.ViewModel.Home.Informations;
using IntelligentHabitacion.App.ViewModel.MyFoods;
using IntelligentHabitacion.App.ViewModel.User.Update;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel.Login
{
    public class UserIsPartOfHomeViewModel : BaseViewModel
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;

        public ICommand CardMyInformationTapped { get; }
        public ICommand CardHomesInformationsTapped { get; }
        public ICommand CardMyFriendsTapped { get; }
        public ICommand CardMyFoodsTapped { get; }
        public ICommand CardCleanHouseTapped { get; }

        public ICommand FloatActionCommand { get; }

        public ICommand LoggoutCommand { get; }
        public ICommand AddNewItemCommand { get; }
        public ICommand AddNewFriendCommand { get; }

        public UserIsPartOfHomeViewModel(Lazy<UserPreferences> userPreferences)
        {
            this.userPreferences = userPreferences;

            LoggoutCommand = new Command(async () => { await ClickLogoutAccount_FloatActionButton(); });
            AddNewItemCommand = new Command(async () => { await OnAddNewItem_FloatActionButton(); });
            AddNewFriendCommand = new Command(async () => { await AddFriends_FloatActionButton(); });

            CardMyInformationTapped = new Command(async () => await ClickOnCardMyInformations());
            CardHomesInformationsTapped = new Command(async () => await ClickOnCardHomesInformations());
            CardMyFriendsTapped = new Command(async () => await ClickOnCardMyFriends());
            CardMyFoodsTapped = new Command(async () => await ClickOnCardMyFoods());
            CardCleanHouseTapped = new Command(async () => await ClickOnCardCleanHouse());

            FloatActionCommand = new Command(async () =>
            {
                var navigation = Resolver.Resolve<INavigation>();
                await navigation.PushPopupAsync(new FloatActionUserIsPartOfHomeModal(LoggoutCommand, AddNewItemCommand, AddNewFriendCommand));
            });
        }

        private async Task ClickOnCardMyInformations()
        {
            await Navigation.PushAsync<UserInformationViewModel>(async (viewModel, _) =>
            {
                await viewModel.Initialize();
            });
        }
        private async Task ClickOnCardHomesInformations()
        {
            await Navigation.PushAsync<HomeInformationViewModel>(async (viewModel, _) =>
            {
                await viewModel.Initialize();
            });
        }
        private async Task ClickOnCardMyFriends()
        {
            await Navigation.PushAsync<MyFriendsViewModel>(async (viewModel, _) =>
            {
                await viewModel.Initialize();
            });
        }
        private async Task ClickOnCardMyFoods()
        {
            await Navigation.PushAsync<MyFoodsViewModel>(async(viewModel, _) =>
            {
                await viewModel.Initialize();
            });
        }
        private async Task ClickOnCardCleanHouse()
        {
        }

        private async Task ClickLogoutAccount_FloatActionButton()
        {
            try
            {
                _userPreferences.Logout();
                Application.Current.MainPage = new NavigationPage((Page)XLabs.Forms.Mvvm.ViewFactory.CreatePage<LoginViewModel, View.Login.LoginPage>(async(viewModel, _) =>
                {
                    await viewModel.Initialize();
                }));
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private async Task OnAddNewItem_FloatActionButton()
        {
            try
            {
                await Navigation.PushAsync<AddEditMyFoodsViewModel>((viewModel, _) =>
                {
                    viewModel.Title = ResourceText.TITLE_NEW_ITEM;
                    viewModel.Model = new FoodModel { Quantity = 1.00m };
                });
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private async Task AddFriends_FloatActionButton()
        {
            try
            {
                await Navigation.PushAsync<AddFriendViewModel>(async (viewModel, _) =>
                {
                    await viewModel.Initialize(null);
                });
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
