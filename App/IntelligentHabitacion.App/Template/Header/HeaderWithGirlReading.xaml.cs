using IntelligentHabitacion.App.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Header
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderWithGirlReading : ContentView
    {
        public HeaderWithGirlReading()
        {
            InitializeComponent();
            
            var deviceWidth = IntelligentHabitacionDevice.IntelligentHabitacionDevice.Width();

            ImageGirlReading.WidthRequest = deviceWidth;
            ImageGirlReading.HeightRequest = deviceWidth * 0.49;

            FillInformations();
        }

        public void FillInformations()
        {
            var userPreferences = XLabs.Ioc.Resolver.Resolve<UserPreferences>();

            LabelUserName.Text = userPreferences.Name;
            ImageKingCrown.IsVisible = userPreferences.IsAdministrator;
        }
    }
}