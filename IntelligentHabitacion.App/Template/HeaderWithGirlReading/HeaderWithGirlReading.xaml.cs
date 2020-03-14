using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.HeaderWithGirlReading
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderWithGirlReading : ContentView
    {
        public HeaderWithGirlReading()
        {
            InitializeComponent();

            var deviceWidth = Application.Current.MainPage.Width;

            ImageGirlReading.WidthRequest = deviceWidth;
            ImageGirlReading.HeightRequest = deviceWidth * 0.77;

            LabelUserName.Text = "Santos";
        }
    }
}