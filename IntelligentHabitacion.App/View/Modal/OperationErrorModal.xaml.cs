using System.ComponentModel;

namespace IntelligentHabitacion.App.View.Modal
{
    [DesignTimeVisible(false)]
    public partial class OperationErrorModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        public OperationErrorModal(string message)
        {
            InitializeComponent();

            LabelText.Text = message;
        }
    }
}
