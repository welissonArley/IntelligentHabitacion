using Rg.Plugins.Popup.Extensions;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.View.Modal.MenuOptions
{
    [DesignTimeVisible(false)]
    public partial class AdministratorFriendDetailModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly ICommand _changeDateOption;
        private readonly ICommand _changeAdministrator;
        private readonly ICommand _removeFriendFromHome;

        public AdministratorFriendDetailModal(ICommand changeDateOption, ICommand changeAdministrator, ICommand removeFriendFromHome)
        {
            InitializeComponent();

            _changeDateOption = changeDateOption;
            _changeAdministrator = changeAdministrator;
            _removeFriendFromHome = removeFriendFromHome;
        }

        private async void ChangeDate_Tapped(object sender, System.EventArgs e)
        {
            await CloseThisModal();
            _changeDateOption.Execute(null);
        }
        private async void ChangeAdministrator_Tapped(object sender, System.EventArgs e)
        {
            await CloseThisModal();
            _changeAdministrator.Execute(null);
        }
        private async void RemoveFriendFromHome_Tapped(object sender, System.EventArgs e)
        {
            await CloseThisModal();
            _removeFriendFromHome.Execute(null);
        }

        private async Task CloseThisModal()
        {
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PopPopupAsync();
        }
    }
}
