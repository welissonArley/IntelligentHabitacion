using IntelligentHabitacion.App.SQLite.Interface;
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
            
            var deviceWidth = IntelligentHabitacionDevice.IntelligentHabitacionDevice.Width();

            ImageGirlReading.WidthRequest = deviceWidth;
            ImageGirlReading.HeightRequest = deviceWidth * 0.77;

            var database = XLabs.Ioc.Resolver.Resolve<ISqliteDatabase>();

            LabelUserName.Text = database.Get().Name;
        }
    }
}