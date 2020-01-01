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

            InputTextChanged();
        }

        private void InputTextChanged()
        {
            InputEmail.TextChanged += (sender, e) =>
            {
                if (string.IsNullOrEmpty(InputEmail.Text))
                    LabelEmail.Text = " ";
                else
                    LabelEmail.Text = ResourceText.TITLE_EMAIL_TWOPOINTS;
            };

            InputPassword.TextChanged += (sender, e) =>
            {
                if (string.IsNullOrEmpty(InputPassword.Text))
                    LabelPassword.Text = " ";
                else
                    LabelPassword.Text = ResourceText.TITLE_PASSWORD_TWOPOINTS;
            };
        }

        private void Clicked_ForgotPassword(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(Resolver.Resolve<ForgotPasswordPage>());
        }
    }
}
