using IntelligentHabitacion.SetOfRules.Interface;
using System.ComponentModel;
using XLabs.Ioc;

namespace IntelligentHabitacion.View.ForgotPassword
{
    [DesignTimeVisible(false)]
    public partial class RequestEmailPage : BasePage
    {
        private readonly ILoginRule _loginRule;

        public RequestEmailPage(ILoginRule loginRule)
        {
            InitializeComponent();

            _loginRule = loginRule;
        }

        private void Button_Clicked_Next(object sender, System.EventArgs e)
        {
            try
            {
                _loginRule.RequestCode(InputEmail.Text);
                Navigation.PushAsync(Resolver.Resolve<ChangePasswordPage>());
            }
            catch (System.Exception exception)
            {
                Exception(exception);
            }
        }
    }
}
