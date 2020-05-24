using System.ComponentModel;

namespace IntelligentHabitacion.App.View.Modal
{
    [DesignTimeVisible(false)]
    public partial class OperationSuccessfullyExecutedModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        public OperationSuccessfullyExecutedModal(string message)
        {
            InitializeComponent();

            LabelText.Text = message;
            CloseWhenBackgroundIsClicked = false;
        }
    }
}
