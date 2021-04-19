using System.ComponentModel;

namespace IntelligentHabitacion.App.View.Modal
{
    [DesignTimeVisible(false)]
    public partial class WithoutInternetConnectionModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        public WithoutInternetConnectionModal()
        {
            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;
        }
    }
}
