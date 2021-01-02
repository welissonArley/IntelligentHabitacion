using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.View.CleanHouse
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingSchedulePage : ContentPage
    {
        public SettingSchedulePage()
        {
            InitializeComponent();

            var width = IntelligentHabitacionDevice.IntelligentHabitacionDevice.Width() - 40;
            var height = width * 0.76;

            Ilustration.WidthRequest = width;
            Ilustration.HeightRequest = height;
        }
    }
}