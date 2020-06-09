using Com.OneSignal;
using Com.OneSignal.Abstractions;
using IntelligentHabitacion.App.OneSignalConfig;
using IntelligentHabitacion.App.SQLite.Interface;
using IntelligentHabitacion.App.View;
using IntelligentHabitacion.App.View.DeleteAccount;
using IntelligentHabitacion.App.View.Friends;
using IntelligentHabitacion.App.View.Friends.Add;
using IntelligentHabitacion.App.View.Modal;
using IntelligentHabitacion.App.View.MyFoods;
using IntelligentHabitacion.App.View.RegisterHome;
using IntelligentHabitacion.App.View.RegisterUser;
using IntelligentHabitacion.App.ViewModel;
using IntelligentHabitacion.App.ViewModel.DeleteAccount;
using IntelligentHabitacion.App.ViewModel.Friends;
using IntelligentHabitacion.App.ViewModel.Friends.Add;
using IntelligentHabitacion.App.ViewModel.Friends.ChangeAdministrator;
using IntelligentHabitacion.App.ViewModel.MyFoods;
using IntelligentHabitacion.App.ViewModel.RegisterHome;
using IntelligentHabitacion.App.ViewModel.RegisterUser;
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

            var user = Resolver.Resolve<ISqliteDatabase>().Get();
            if (user != null)
            {
                if (user.IsPartOfOneHome)
                    MainPage = new NavigationPage((Page)ViewFactory.CreatePage<UserIsPartOfHomeViewModel, UserIsPartOfHomePage>());
                else
                    MainPage = new NavigationPage((Page)ViewFactory.CreatePage<UserWithoutPartOfHomeViewModel, UserWithoutPartOfHomePage>());
            }
            else
                MainPage = new GetStartedPage();

            Resolver.Resolve<IDependencyContainer>()
                .Register<INavigationService>(t => new NavigationService(MainPage.Navigation)) // New Xlabs nav service
                .Register(t => MainPage.Navigation); // Old Xlabs nav service
        }

        private void RegisterViews()
        {
            ViewFactory.Register<LoginPage, LoginViewModel>();
            ViewFactory.Register<View.ForgotPassword.RequestEmailPage, ViewModel.ForgotPassword.RequestEmailViewModel>();
            ViewFactory.Register<View.ForgotPassword.ResetPasswordPage, ViewModel.ForgotPassword.ResetPasswordViewModel>();
            ViewFactory.Register<RequestNamePage, RequestNameViewModel>();
            ViewFactory.Register<RequestPhoneNumberPage, RequestPhoneNumberViewModel>();
            ViewFactory.Register<RequestEmailPage, RequestEmailViewModel>();
            ViewFactory.Register<RequestEmergencyContact1Page, RequestEmergencyContact1ViewModel>();
            ViewFactory.Register<RequestEmergencyContact2Page, RequestEmergencyContact2ViewModel>();
            ViewFactory.Register<RequestPasswordPage, RequestPasswordViewModel>();
            ViewFactory.Register<UserWithoutPartOfHomePage, UserWithoutPartOfHomeViewModel>();
            ViewFactory.Register<RequestZipCodePage, RequestZipCodeViewModel>();
            ViewFactory.Register<RequestCityPage, RequestCityViewModel>();
            ViewFactory.Register<RequestAddressPage, RequestAddressViewModel>();
            ViewFactory.Register<RequestNumberPage, RequestNumberViewModel>();
            ViewFactory.Register<RequestComplementPage, RequestComplementViewModel>();
            ViewFactory.Register<RequestNeighborhoodPage, RequestNeighborhoodViewModel>();
            ViewFactory.Register<RequestNetworkInformationPage, RequestNetworkInformationViewModel>();
            ViewFactory.Register<UpdateUserInformationPage, UpdateUserInformationViewModel>();
            ViewFactory.Register<ConfirmDeleteAccountPage, ConfirmDeleteAccountViewModel>();
            ViewFactory.Register<DeleteAccountPage, DeleteAccountViewModel>();
            ViewFactory.Register<ChangePasswordPage, ChangePasswordViewModel>();
            ViewFactory.Register<UserIsPartOfHomePage, UserIsPartOfHomeViewModel>();
            ViewFactory.Register<HomeInformationPage, HomeInformationViewModel>();
            ViewFactory.Register<MyFriendsPage, MyFriendsViewModel>();
            ViewFactory.Register<FriendDetailsPage, FriendDetailsViewModel>();
            ViewFactory.Register<MyFoodsPage, MyFoodsViewModel>();
            ViewFactory.Register<AddEditMyFoodsPage, AddEditMyFoodsViewModel>();
            ViewFactory.Register<QrCodeToAddFriendPage, QrCodeToAddFriendViewModel>();
            ViewFactory.Register<AcceptNewFriendPage, AcceptNewFriendViewModel>();
            ViewFactory.Register<ChangeAdministratorPage, ChangeAdministratorViewModel>();
            ViewFactory.Register<RemoveFriendFromHomePage, RemoveFriendFromHomeViewModel>();
            ViewFactory.Register<ApproveActionWithCodePasswordPage, ApproveActionWithCodePasswordViewModel>();
        }

        private static void OneSignalId(string playerID, string pushToken)
        {
            OneSignalManager.SetMyIdOneSignal(playerID);
        }
    }
}
