using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.View.Modal.MenuOptions;
using IntelligentHabitacion.App.ViewModel.CleanHouse;
using IntelligentHabitacion.App.ViewModel.Friends;
using IntelligentHabitacion.App.ViewModel.Friends.Add;
using IntelligentHabitacion.App.ViewModel.MyFoods;
using IntelligentHabitacion.App.ViewModel.User.Update;
using IntelligentHabitacion.Communication.Response;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Linq;
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
            try
            {
                await Navigation.PushAsync<UserInformationViewModel>(async (viewModel, _) =>
                {
                    await viewModel.Initialize();
                });
            }
            catch (System.Exception exeption)
            {
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
                await Navigation.PushAsync<MyFoodsViewModel>(async(viewModel, _) =>
                {
                    await viewModel.Initialize();
                });
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private async Task ClickOnCardCleanHouse()
        {
            try
            {
                await ShowLoading();

                var cleaningScheduleRule = Resolver.Resolve<ICleaningScheduleRule>();
                var response = await cleaningScheduleRule.GetMyTasks();

                await Navigation.PushAsync<MyTasksViewModel>((viewModel, page) =>
                {
                    if (!(response as ResponseNeedActionJson is null))
                    {
                        var action = (ResponseNeedActionJson)response;

                        viewModel.ScheduleCreated = false;
                        viewModel.InfoMessage = action.Message;
                        viewModel.Action = action.Action;
                    }
                    else
                    {
                        var action = (ResponseMyTasksCleaningScheduleJson)response;

                        viewModel.ScheduleCreated = true;
                        viewModel.Action = null;

                        viewModel.Model = new Model.MyTasksCleanHouseModel
                        {
                            Name = viewModel.Name,
                            Month = System.DateTime.UtcNow,
                            Tasks = new System.Collections.ObjectModel.ObservableCollection<Model.TasksForTheMonth>(action.Tasks.Select(c => new Model.TasksForTheMonth
                            {
                                Room = c.Room, CleaningRecords = c.CleaningRecords, LastRecord = c.LastRecord, Id = c.Id
                            }))
                        };
                    }
                });
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
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
                await ShowLoading();
                await Navigation.PushAsync<AddEditMyFoodsViewModel>((viewModel, page) =>
                {
                    viewModel.Title = ResourceText.TITLE_NEW_ITEM;
                    viewModel.Model = new FoodModel { Quantity = 1.00m };
                });
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
        private async Task AddFriends_FloatActionButton()
        {
            try
            {
                await ShowLoading();
                await Navigation.PushAsync<QrCodeToAddFriendViewModel>();
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
