using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.View.Login;
using IntelligentHabitacion.App.ViewModel;
using IntelligentHabitacion.App.ViewModel.Login;
using Plugin.Fingerprint;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InitializePage : ContentPage
    {
        public InitializePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var userPreferences = Resolver.Resolve<UserPreferences>();
            if (await userPreferences.HasAccessToken())
            {
                if (userPreferences.IsPartOfOneHome)
                    Application.Current.MainPage = new NavigationPage((Page)ViewFactory.CreatePage<UserIsPartOfHomeViewModel, UserIsPartOfHomePage>());
                else
                    Application.Current.MainPage = new NavigationPage((Page)ViewFactory.CreatePage<UserWithoutPartOfHomeViewModel, UserWithoutPartOfHomePage>());
            }
            else
            {
                Application.Current.MainPage = new NavigationPage((Page)ViewFactory.CreatePage<GetStartedViewModel, GetStartedPage>());
                if (await userPreferences.AlreadySignedIn() && await CrossFingerprint.Current.IsAvailableAsync())
                {
                    await Application.Current.MainPage.Navigation.PushAsync((Page)ViewFactory.CreatePage<LoginViewModel, LoginPage>(async (viewModel, page) =>
                    {
                        await viewModel.Initialize();
                    }));
                }
            }
        }
    }
}