using System.ComponentModel;
using Xamarin.Forms;

namespace IntelligentHabitacion.View
{
    [DesignTimeVisible(false)]
    public partial class GetStartedPage : BasePage
    {
        public GetStartedPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked_GetStarted(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}
