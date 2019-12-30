using IntelligentHabitacion.View;
using System.ComponentModel;
using Xamarin.Forms;

namespace IntelligentHabitacion
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked_GetStarted(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}
