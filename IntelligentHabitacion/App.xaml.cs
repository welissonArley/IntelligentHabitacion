using IntelligentHabitacion.View;
using IntelligentHabitacion.View.ForgotPassword;
using IntelligentHabitacion.ViewModel;
using IntelligentHabitacion.ViewModel.ForgotPassword;
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
                .Register<INavigationService>(t => new NavigationService(MainPage.Navigation)) //New Xlabs nav service
                .Register(t => MainPage.Navigation); // old Xlabs nav service
        }

        private void RegisterViews()
        {
            ViewFactory.Register<LoginPage, LoginViewModel>();
            ViewFactory.Register<RequestEmailPage, RequestEmailViewModel>();
            ViewFactory.Register<ChangePasswordPage, ChangePasswordViewModel>();
        }
    }
}
