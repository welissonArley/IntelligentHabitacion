using Rg.Plugins.Popup.Extensions;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.View.Modal.MenuOptions
{
    [DesignTimeVisible(false)]
    public partial class FloatActionTaskCleaningScheduleModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        private ICommand RegisterRoomsCleanedCommand { get; }

        public FloatActionTaskCleaningScheduleModal(ICommand registerRoomsCleanedCommand)
        {
            InitializeComponent();

            RegisterRoomsCleanedCommand = registerRoomsCleanedCommand;
        }

        private async Task CloseThisModal()
        {
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PopPopupAsync();
        }

        private async void RegisterRoomsCleaned_Tapped(object sender, System.EventArgs e)
        {
            await CloseThisModal();
            RegisterRoomsCleanedCommand.Execute(null);
        }
    }
}
