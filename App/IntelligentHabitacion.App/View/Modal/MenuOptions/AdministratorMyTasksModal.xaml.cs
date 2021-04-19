using Rg.Plugins.Popup.Extensions;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.View.Modal.MenuOptions
{
    [DesignTimeVisible(false)]
    public partial class AdministratorMyTasksModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly ICommand _manageScheduleOption;
        private readonly ICommand _settingsOption;

        public AdministratorMyTasksModal(ICommand manageScheduleOption, ICommand settingsOption)
        {
            InitializeComponent();

            _manageScheduleOption = manageScheduleOption;
            _settingsOption = settingsOption;
        }

        private async void ManageSchedule_Tapped(object sender, System.EventArgs e)
        {
            await CloseThisModal();
            _manageScheduleOption.Execute(null);
        }
        private async void Settings_Tapped(object sender, System.EventArgs e)
        {
            await CloseThisModal();
            _settingsOption.Execute(null);
        }

        private async Task CloseThisModal()
        {
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PopPopupAsync();
        }
    }
}
