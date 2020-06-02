using System.ComponentModel;
using System.Windows.Input;

namespace IntelligentHabitacion.App.View.Modal.MenuOptions
{
    [DesignTimeVisible(false)]
    public partial class AdministratorFriendDetailModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly ICommand _changeDateOption;

        public AdministratorFriendDetailModal(ICommand changeDateOption)
        {
            InitializeComponent();

            _changeDateOption = changeDateOption;
        }

        private void ChangeDate_Tapped(object sender, System.EventArgs e)
        {
            _changeDateOption.Execute(null);
        }
    }
}
