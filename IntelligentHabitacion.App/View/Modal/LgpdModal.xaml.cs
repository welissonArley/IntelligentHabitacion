using Rg.Plugins.Popup.Extensions;
using System.ComponentModel;

namespace IntelligentHabitacion.App.View.Modal
{
    [DesignTimeVisible(false)]
    public partial class LgpdModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        public LgpdModal(string message)
        {
            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;
            Message.Text = message;
        }

        private void OnButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
    }
}
