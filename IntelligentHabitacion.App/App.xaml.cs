using Com.OneSignal;
using Com.OneSignal.Abstractions;
using IntelligentHabitacion.App.OneSignalConfig;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.View;
using IntelligentHabitacion.App.View.CleanHouse;
using IntelligentHabitacion.App.View.Friends;
using IntelligentHabitacion.App.View.Friends.Add;
using IntelligentHabitacion.App.View.Home.Delete;
using IntelligentHabitacion.App.View.Login;
using IntelligentHabitacion.App.View.Modal;
using IntelligentHabitacion.App.View.MyFoods;
using IntelligentHabitacion.App.View.User.Delete;
using IntelligentHabitacion.App.View.User.Register;
using IntelligentHabitacion.App.View.User.Update;
using IntelligentHabitacion.App.ViewModel;
using IntelligentHabitacion.App.ViewModel.CleanHouse;
using IntelligentHabitacion.App.ViewModel.Friends;
using IntelligentHabitacion.App.ViewModel.Friends.Add;
using IntelligentHabitacion.App.ViewModel.Friends.ChangeAdministrator;
using IntelligentHabitacion.App.ViewModel.Home.Delete;
using IntelligentHabitacion.App.ViewModel.Login;
using IntelligentHabitacion.App.ViewModel.MyFoods;
using IntelligentHabitacion.App.ViewModel.User.Delete;
using IntelligentHabitacion.App.ViewModel.User.Register;
using IntelligentHabitacion.App.ViewModel.User.Update;
using Plugin.Fingerprint;
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

            var userPreferences = Resolver.Resolve<UserPreferences>();
            if (userPreferences.HasAccessToken)
            {
                if (userPreferences.IsPartOfOneHome)
                    MainPage = new NavigationPage((Page)ViewFactory.CreatePage<UserIsPartOfHomeViewModel, UserIsPartOfHomePage>());
                else
                    MainPage = new NavigationPage((Page)ViewFactory.CreatePage<UserWithoutPartOfHomeViewModel, UserWithoutPartOfHomePage>());
            }
            else
            {
                MainPage = new NavigationPage((Page)ViewFactory.CreatePage<GetStartedViewModel, GetStartedPage>());
                if (userPreferences.AlreadySignedIn && CrossFingerprint.Current.IsAvailableAsync().GetAwaiter().GetResult())
                    MainPage.Navigation.PushAsync((Page)ViewFactory.CreatePage<LoginViewModel, LoginPage>());
            }

            Resolver.Resolve<IDependencyContainer>()
                .Register<INavigationService>(t => new NavigationService(MainPage.Navigation)) // New Xlabs nav service
                .Register(t => MainPage.Navigation); // Old Xlabs nav service
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
            ViewFactory.Register<ConfirmDeleteAccountPage, ConfirmDeleteAccountViewModel>();
            ViewFactory.Register<DeleteAccountPage, DeleteAccountViewModel>();
            ViewFactory.Register<ChangePasswordPage, ChangePasswordViewModel>();
            ViewFactory.Register<UserIsPartOfHomePage, UserIsPartOfHomeViewModel>();
            ViewFactory.Register<View.Home.Informations.Brazil.HomeInformationPage, ViewModel.Home.Informations.Brazil.HomeInformationViewModel>();
            ViewFactory.Register<View.Home.Informations.Others.HomeInformationPage, ViewModel.Home.Informations.Others.HomeInformationViewModel>();
            ViewFactory.Register<MyFriendsPage, MyFriendsViewModel>();
            ViewFactory.Register<FriendDetailsPage, FriendDetailsViewModel>();
            ViewFactory.Register<MyFoodsPage, MyFoodsViewModel>();
            ViewFactory.Register<AddEditMyFoodsPage, AddEditMyFoodsViewModel>();
            ViewFactory.Register<QrCodeToAddFriendPage, QrCodeToAddFriendViewModel>();
            ViewFactory.Register<AcceptNewFriendPage, AcceptNewFriendViewModel>();
            ViewFactory.Register<ChangeAdministratorPage, ChangeAdministratorViewModel>();
            ViewFactory.Register<RemoveFriendFromHomePage, RemoveFriendFromHomeViewModel>();
            ViewFactory.Register<ApproveActionWithCodePasswordPage, ApproveActionWithCodePasswordViewModel>();
            ViewFactory.Register<DeleteHomePage, DeleteHomeViewModel>();
            ViewFactory.Register<View.Home.Register.Others.RegisterHomePage, ViewModel.Home.Register.Others.RegisterHomeViewModel>();
            ViewFactory.Register<View.Home.Register.Brazil.RegisterHomePage, ViewModel.Home.Register.Brazil.RegisterHomeViewModel>();
            ViewFactory.Register<View.Home.Register.Brazil.RequestZipCodePage, ViewModel.Home.Register.Brazil.RequestZipCodeViewModel>();
            ViewFactory.Register<UserSchedulePage, UserScheduleViewModel>();
            ViewFactory.Register<RatingCleaningPage, RatingCleaningViewModel>();
            ViewFactory.Register<SettingSchedulePage, SettingScheduleViewModel>();
        }

        private static void OneSignalId(string playerID, string pushToken)
        {
            OneSignalManager.SetMyIdOneSignal(playerID);
        }
    }
}
