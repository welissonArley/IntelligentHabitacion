using IntelligentHabitacion.App.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.View.Dashboard.PartOfHome
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserIsPartOfHomeFlyoutPageFlyout : ContentPage
    {
        public UserIsPartOfHomeFlyoutPageFlyout()
        {
            InitializeComponent();

            var userPreferences = Resolver.Resolve<UserPreferences>();
            OptionAddFriend.IsVisible = userPreferences.IsAdministrator;
        }
    }
}