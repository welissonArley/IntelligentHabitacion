using IntelligentHabitacion.App.View;
using IntelligentHabitacion.App.View.DeleteAccount;
using IntelligentHabitacion.App.View.ForgotPassword;
using IntelligentHabitacion.App.View.RegisterHome;
using IntelligentHabitacion.App.View.RegisterUser;
using IntelligentHabitacion.App.ViewModel;
using IntelligentHabitacion.App.ViewModel.DeleteAccount;
using IntelligentHabitacion.App.ViewModel.ForgotPassword;
using IntelligentHabitacion.App.ViewModel.RegisterHome;
using IntelligentHabitacion.App.ViewModel.RegisterUser;
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

            RegisterViews();

            MainPage = new GetStartedPage();

            Resolver.Resolve<IDependencyContainer>()
                .Register<INavigationService>(t => new NavigationService(MainPage.Navigation)) // New Xlabs nav service
                .Register(t => MainPage.Navigation); // Old Xlabs nav service
        }

        private void RegisterViews()
        {
            ViewFactory.Register<LoginPage, LoginViewModel>();
            ViewFactory.Register<View.ForgotPassword.RequestEmailPage, ViewModel.ForgotPassword.RequestEmailViewModel>();
            ViewFactory.Register<ChangePasswordPage, ChangePasswordViewModel>();
            ViewFactory.Register<RequestNamePage, RequestNameViewModel>();
            ViewFactory.Register<RequestPhoneNumberPage, RequestPhoneNumberViewModel>();
            ViewFactory.Register<View.RegisterUser.RequestEmailPage, ViewModel.RegisterUser.RequestEmailViewModel>();
            ViewFactory.Register<RequestEmergencyContact1Page, RequestEmergencyContact1ViewModel>();
            ViewFactory.Register<RequestEmergencyContact2Page, RequestEmergencyContact2ViewModel>();
            ViewFactory.Register<RequestPasswordPage, RequestPasswordViewModel>();
            ViewFactory.Register<UserWithoutPartOfHomePage, UserWithoutPartOfHomePageViewModel>();
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
        }
    }
}
