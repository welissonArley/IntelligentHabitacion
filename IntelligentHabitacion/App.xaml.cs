using IntelligentHabitacion.View;
using IntelligentHabitacion.View.ForgotPassword;
using IntelligentHabitacion.View.RegisterUser;
using IntelligentHabitacion.ViewModel;
using IntelligentHabitacion.ViewModel.ForgotPassword;
using IntelligentHabitacion.ViewModel.RegisterUser;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Forms.Services;
using XLabs.Ioc;
using XLabs.Platform.Services;

namespace IntelligentHabitacion
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
        }
    }
}
