using Com.OneSignal;
using Com.OneSignal.Abstractions;
using IntelligentHabitacion.App.OneSignalConfig;
using IntelligentHabitacion.App.View;
using IntelligentHabitacion.App.View.CleaningSchedule;
using IntelligentHabitacion.App.View.Friends;
using IntelligentHabitacion.App.View.Home.Informations;
using IntelligentHabitacion.App.View.Home.Register;
using IntelligentHabitacion.App.View.Login;
using IntelligentHabitacion.App.View.Modal;
using IntelligentHabitacion.App.View.MyFoods;
using IntelligentHabitacion.App.View.User.Register;
using IntelligentHabitacion.App.View.User.Update;
using IntelligentHabitacion.App.ViewModel;
using IntelligentHabitacion.App.ViewModel.CleaningSchedule;
using IntelligentHabitacion.App.ViewModel.Friends;
using IntelligentHabitacion.App.ViewModel.Friends.ChangeAdministrator;
using IntelligentHabitacion.App.ViewModel.Home.Informations;
using IntelligentHabitacion.App.ViewModel.Home.Register;
using IntelligentHabitacion.App.ViewModel.Login;
using IntelligentHabitacion.App.ViewModel.MyFoods;
using IntelligentHabitacion.App.ViewModel.User.Register;
using IntelligentHabitacion.App.ViewModel.User.Update;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Forms.Services;
using XLabs.Ioc;
using XLabs.Platform.Services;

namespace IntelligentHabitacion.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            OneSignal.Current.StartInit(OneSignalManager.OneSignalKey)
                .InFocusDisplaying(OSInFocusDisplayOption.None)
                .HandleNotificationReceived((notification) =>
                {
                    OneSignalManager.Notification(notification);
                    if (!notification.shown) // if the "show" is false, this means that the app is in focus.
                        Current.MainPage.Navigation.PushPopupAsync(new NotifyModal(notification.payload.title, notification.payload.body));
                }).EndInit();
            OneSignal.Current.RegisterForPushNotifications();

            OneSignal.Current.IdsAvailable(OneSignalId);

            RegisterViews();

            Resolver.Resolve<IDependencyContainer>()
                .Register<INavigationService>(t => new NavigationService(MainPage.Navigation)) // New Xlabs nav service
                .Register(t => MainPage.Navigation); // Old Xlabs nav service

            MainPage = new InitializePage();
        }

        private void RegisterViews()
        {
            ViewFactory.Register<GetStartedPage, GetStartedViewModel>();
            ViewFactory.Register<LoginPage, LoginViewModel>();
            ViewFactory.Register<View.User.ForgotPassword.RequestEmailPage, ViewModel.User.ForgotPassword.RequestEmailViewModel>();
            ViewFactory.Register<View.User.ForgotPassword.ResetPasswordPage, ViewModel.User.ForgotPassword.ResetPasswordViewModel>();
            ViewFactory.Register<RequestNamePage, RequestNameViewModel>();
            ViewFactory.Register<RequestPhoneNumberPage, RequestPhoneNumberViewModel>();
            ViewFactory.Register<RequestEmailPage, RequestEmailViewModel>();
            ViewFactory.Register<RequestEmergencyContact1Page, RequestEmergencyContact1ViewModel>();
            ViewFactory.Register<RequestEmergencyContact2Page, RequestEmergencyContact2ViewModel>();
            ViewFactory.Register<RequestPasswordPage, RequestPasswordViewModel>();
            ViewFactory.Register<UserWithoutPartOfHomePage, UserWithoutPartOfHomeViewModel>();
            ViewFactory.Register<UserInformationPage, UserInformationViewModel>();
            ViewFactory.Register<ChangePasswordPage, ChangePasswordViewModel>();
            ViewFactory.Register<UserIsPartOfHomePage, UserIsPartOfHomeViewModel>();
            ViewFactory.Register<MyFriendsPage, MyFriendsViewModel>();
            ViewFactory.Register<FriendInformationsDetailsPage, FriendInformationsDetailsViewModel>();
            ViewFactory.Register<MyFoodsPage, MyFoodsViewModel>();
            ViewFactory.Register<AddEditMyFoodsPage, AddEditMyFoodsViewModel>();
            ViewFactory.Register<AddFriendPage, AddFriendViewModel>();
            ViewFactory.Register<RemoveFriendFromHomePage, RemoveFriendFromHomeViewModel>();
            ViewFactory.Register<RegisterHomePage, RegisterHomeViewModel>();
            ViewFactory.Register<InsertRoomPage, InsertRoomViewModel>();
            ViewFactory.Register<SelectCountryPage, SelectCountryViewModel>();
            ViewFactory.Register<HomeInformationPage, HomeInformationViewModel>();
            ViewFactory.Register<TasksPage, TasksViewModel>();
            ViewFactory.Register<SelectOptionsCleaningHousePage, SelectOptionsCleaningHouseViewModel>();
            ViewFactory.Register<SelectRoomsRegisterCleanedPage, SelectRoomsRegisterCleanedViewModel>();
            ViewFactory.Register<TaskDetailsPage, TaskDetailsViewModel>();
            ViewFactory.Register<CompleteHistoryPage, CompleteHistoryViewModel>();
        }

        private static void OneSignalId(string playerID, string pushToken)
        {
            OneSignalManager.SetMyIdOneSignal(playerID);
        }
    }
}
