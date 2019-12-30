using IntelligentHabitacion.Template;
using RoundedBoxView.Forms.Plugin.Abstractions;
using System.ComponentModel;
using Xamarin.Forms;

namespace IntelligentHabitacion.View
{
    [DesignTimeVisible(false)]
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();

            InputTextChanged();
        }

        private void InputTextChanged()
        {
            InputCodeReceived.TextChanged += (sender, e) =>
            {
                if (string.IsNullOrEmpty(InputCodeReceived.Text))
                    LabelCodeReceived.Text = " ";
                else
                    LabelCodeReceived.Text = ResourceText.TITLE_CODE_RECEIVED_TWOPOINTS;
            };

            InputNewPassword.TextChanged += (sender, e) =>
            {
                if (string.IsNullOrEmpty(LabelNewPassword.Text))
                    LabelNewPassword.Text = " ";
                else
                    LabelNewPassword.Text = ResourceText.TITLE_NEW_PASSWORD_TWOPOINTS;
            };

            InputConfirmationPassword.TextChanged += (sender, e) =>
            {
                if (string.IsNullOrEmpty(LabelPasswordConfirmation.Text))
                    LabelPasswordConfirmation.Text = " ";
                else
                    LabelPasswordConfirmation.Text = ResourceText.TITLE_PASSWORD_CONFIRMATION_TWOPOINTS;
            };
        }

        private void Button_Clicked_Next(object sender, System.EventArgs e)
        {
            StepContent.Children.RemoveAt(1);

            StepContent.Children.Insert(0, new PrevOrNextStep
            {
                Margin = new Thickness(0,0,15,0)
            });

            RequestEmailContent.IsVisible = false;
            ChangePasswordContent.IsVisible = true;
        }

        private void Button_Clicked_Change(object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
