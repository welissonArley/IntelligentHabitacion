using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.HeaderWithGirlReading
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderWithGirlReading : ContentView
    {
        public bool ShowImageKingCrown
        {
            set
            {
                ImageKingCrown.IsVisible = value;
            }
            get { return ImageKingCrown.IsVisible; }
        }

        public string UserName
        {
            set
            {
                LabelUserName.Text = value;
            }
            get { return LabelUserName.Text; }
        }

        public HeaderWithGirlReading()
        {
            InitializeComponent();

            var deviceWidth = Application.Current.MainPage.Width;

            ImageGirlReading.WidthRequest = deviceWidth;
            ImageGirlReading.HeightRequest = deviceWidth * 0.77;
        }
    }
}