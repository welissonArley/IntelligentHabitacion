using IntelligentHabitacion.App.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.View.CleanHouse
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyTasksPage : ContentPage
    {
        public MyTasksPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var userPreferences = XLabs.Ioc.Resolver.Resolve<UserPreferences>();
            if (!userPreferences.IsAdministrator)
                ButtonCreateSchedule.IsVisible = false;

            base.OnAppearing();
        }
    }
}