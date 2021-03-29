using Rg.Plugins.Popup.Extensions;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.View.Modal.MenuOptions
{
    [DesignTimeVisible(false)]
    public partial class FloatActionTaskCleaningScheduleModal : Rg.Plugins.Popup.Pages.PopupPage
    {

        public FloatActionTaskCleaningScheduleModal()
        {
            InitializeComponent();
        }

        private async Task CloseThisModal()
        {
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PopPopupAsync();
        }
    }
}
