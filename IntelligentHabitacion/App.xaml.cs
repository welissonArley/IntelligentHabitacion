using IntelligentHabitacion.View;
using Xamarin.Forms;

namespace IntelligentHabitacion
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new GetStartedPage());
        }
    }
}
