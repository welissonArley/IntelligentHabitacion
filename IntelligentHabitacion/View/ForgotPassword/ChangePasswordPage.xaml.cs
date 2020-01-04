using IntelligentHabitacion.SetOfRules.Interface;
using System.ComponentModel;

namespace IntelligentHabitacion.View.ForgotPassword
{
    [DesignTimeVisible(false)]
    public partial class ChangePasswordPage : BasePage
    {
        private readonly ILoginRule _loginRule;

        public ChangePasswordPage(ILoginRule loginRule)
        {
            InitializeComponent();

            _loginRule = loginRule;
        }

        private void Button_Clicked_Change(object sender, System.EventArgs e)
        {
            try
            {
                _loginRule.ChangePasswordForgetPassword(CodeReceivedContent.TypedText(), NewPasswordContent.TypedText(), ConfirmationPasswordContent.TypedText());
                Navigation.PopToRootAsync();
            }
            catch (System.Exception exception)
            {
                Exception(exception);
            }
        }
    }
}
