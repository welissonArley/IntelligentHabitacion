using System.ComponentModel;
using System.Windows.Input;

namespace IntelligentHabitacion.App.View.Modal.MenuOptions
{
    [DesignTimeVisible(false)]
    public partial class AdministratorFriendDetailModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly ICommand _changeDateOption;
        private readonly ICommand _changeAdministrator;

        public AdministratorFriendDetailModal(ICommand changeDateOption, ICommand changeAdministrator)
        {
            InitializeComponent();

            _changeDateOption = changeDateOption;
            _changeAdministrator = changeAdministrator;
        }

        private void ChangeDate_Tapped(object sender, System.EventArgs e)
        {
            _changeDateOption.Execute(null);
        }
        private void ChangeAdministrator_Tapped(object sender, System.EventArgs e)
        {
            _changeAdministrator.Execute(null);
        }
    }
}
