using System.ComponentModel;

namespace IntelligentHabitacion.App.View.Modal
{
    [DesignTimeVisible(false)]
    public partial class QuickInformationModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        public QuickInformationModal(string message)
        {
            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;
            MessageLabel.Text = message;
        }
    }
}
