using IntelligentHabitacion.SetOfRules.Interface;
using System.ComponentModel;

namespace IntelligentHabitacion.View.RegisterUser
{
    [DesignTimeVisible(false)]
    public partial class RequestPhoneNumberPage : BasePage
    {
        private readonly IUserRule _userRule;

        public RequestPhoneNumberPage(IUserRule userRule)
        {
            InitializeComponent();

            _userRule = userRule;
        }

        private void Button_Clicked_Next(object sender, System.EventArgs e)
        {
            try
            {
                
            }
            catch (System.Exception exception)
            {
                Exception(exception);
            }
        }
    }
}
