using IntelligentHabitacion.SetOfRules.Interface;
using IntelligentHabitacion.Template;
using System.ComponentModel;
using Xamarin.Forms;

namespace IntelligentHabitacion.View
{
    [DesignTimeVisible(false)]
    public partial class ForgotPasswordPage : BasePage
    {
        private readonly ILoginRule _loginRule;

        public ForgotPasswordPage(ILoginRule loginRule)
        {
            InitializeComponent();

            _loginRule = loginRule;
        }

        private void Button_Clicked_Next(object sender, System.EventArgs e)
        {
            try
            {
                _loginRule.RequestCode(InputEmail.Text);

                StepContent.Children.RemoveAt(1);

                StepContent.Children.Insert(0, new PrevOrNextStep
                {
                    Margin = new Thickness(0, 0, 15, 0)
                });

                RequestEmailContent.IsVisible = false;
                ChangePasswordContent.IsVisible = true;
            }
            catch (System.Exception exception)
            {
                Exception(exception);
            }
        }

        private void Button_Clicked_Change(object sender, System.EventArgs e)
        {
            try
            {
                _loginRule.ChangePasswordForgetPassword(CodeReceivedContent.TypedText(), NewPasswordContent.TypedText(), ConfirmationPasswordContent.TypedText());
                Navigation.PopAsync();
            }
            catch(System.Exception exception)
            {
                Exception(exception);
            }
        }
    }
}
