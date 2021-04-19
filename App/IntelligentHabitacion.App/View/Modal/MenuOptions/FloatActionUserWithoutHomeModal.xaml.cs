using Rg.Plugins.Popup.Extensions;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.View.Modal.MenuOptions
{
    [DesignTimeVisible(false)]
    public partial class FloatActionUserWithoutHomeModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        private ICommand LoggoutCommand { get; }

        public FloatActionUserWithoutHomeModal(ICommand loggoutCommand)
        {
            InitializeComponent();

            LoggoutCommand = loggoutCommand;
        }

        private async Task CloseThisModal()
        {
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PopPopupAsync();
        }

        private async void Loggout_Tapped(object sender, System.EventArgs e)
        {
            await CloseThisModal();
            LoggoutCommand.Execute(null);
        }
    }
}
