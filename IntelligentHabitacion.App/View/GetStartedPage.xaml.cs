using IntelligentHabitacion.App.ViewModel;
using System.ComponentModel;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

namespace IntelligentHabitacion.App.View
{
    [DesignTimeVisible(false)]
    public partial class GetStartedPage : ContentPage
    {
        public GetStartedPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked_GetStarted(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage((Page)ViewFactory.CreatePage<LoginViewModel, LoginPage>());
        }
    }
}
