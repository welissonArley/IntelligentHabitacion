using IntelligentHabitacion.SetOfRules.Interface;
using System.ComponentModel;
using XLabs.Ioc;

namespace IntelligentHabitacion.View.RegisterUser
{
    [DesignTimeVisible(false)]
    public partial class RequestNamePage : BasePage
    {
        private readonly IUserRule _userRule;

        public RequestNamePage(IUserRule userRule)
        {
            InitializeComponent();

            _userRule = userRule;
        }

        private void Button_Clicked_Next(object sender, System.EventArgs e)
        {
            try
            {
                _userRule.ValidateName(InputName.Text);
                Navigation.PushAsync(Resolver.Resolve<RequestPhoneNumberPage>());
            }
            catch (System.Exception exception)
            {
                Exception(exception);
            }
        }
    }
}
