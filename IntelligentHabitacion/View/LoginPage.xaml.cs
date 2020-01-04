using IntelligentHabitacion.View.ForgotPassword;
using IntelligentHabitacion.View.RegisterUser;
using System.ComponentModel;
using XLabs.Ioc;

namespace IntelligentHabitacion.View
{
    [DesignTimeVisible(false)]
    public partial class LoginPage : BasePage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Clicked_ForgotPassword(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(Resolver.Resolve<RequestEmailPage>());
        }

        private void Clicked_RegisterUser(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(Resolver.Resolve<RequestNamePage>());
        }
    }
}
