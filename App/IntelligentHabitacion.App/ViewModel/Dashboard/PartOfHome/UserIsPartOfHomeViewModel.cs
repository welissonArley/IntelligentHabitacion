using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.ViewModel.AboutThisProject;
using IntelligentHabitacion.App.ViewModel.ContactUs;
using IntelligentHabitacion.App.ViewModel.Friends;
using IntelligentHabitacion.App.ViewModel.Login;
using IntelligentHabitacion.App.ViewModel.MyFoods;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.Dashboard.PartOfHome
{
    public class UserIsPartOfHomeViewModel : BaseViewModel
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;

        public ICommand LoggoutCommand { get; }
        public ICommand AddNewItemCommand { get; }
        public ICommand AddNewFriendCommand { get; }
        public ICommand AboutThisProjectCommand { get; }
        public ICommand ContactUsCommand { get; }

        public UserIsPartOfHomeViewModel(Lazy<UserPreferences> userPreferences)
        {
            this.userPreferences = userPreferences;

            LoggoutCommand = new Command(async () => { await ClickLogoutAccount(); });
            AddNewItemCommand = new Command(async () => { await OnAddNewItem(); });
            AddNewFriendCommand = new Command(async () => { await AddFriends(); });
            ContactUsCommand = new Command(async () => { await ContactUs(); });
            AboutThisProjectCommand = new Command(async () => { await AboutThisProject(); });
        }

        private async Task ClickLogoutAccount()
        {
            try
            {
                _userPreferences.Logout();
                Application.Current.MainPage = new NavigationPage((Page)XLabs.Forms.Mvvm.ViewFactory.CreatePage<LoginViewModel, View.Login.LoginPage>(async (viewModel, _) =>
                {
                    await viewModel.Initialize();
                }));
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private async Task OnAddNewItem()
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
        private async Task AddFriends()
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
        private async Task ContactUs()
        {
            try
            {
                await Navigation.PushAsync<ContactUsViewModel>();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private async Task AboutThisProject()
        {
            try
            {
                await Navigation.PushAsync<ProjectInformationsViewModel>();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
