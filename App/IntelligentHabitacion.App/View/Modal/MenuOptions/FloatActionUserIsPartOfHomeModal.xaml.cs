using IntelligentHabitacion.App.Services;
using Rg.Plugins.Popup.Extensions;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.View.Modal.MenuOptions
{
    [DesignTimeVisible(false)]
    public partial class FloatActionUserIsPartOfHomeModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        private ICommand LoggoutCommand { get; }
        private ICommand AddNewItemCommand { get; }
        private ICommand AddNewFriendCommand { get; }

        public FloatActionUserIsPartOfHomeModal(ICommand loggoutCommand, ICommand addNewItemCommand, ICommand addNewFriendCommand)
        {
            InitializeComponent();

            LoggoutCommand = loggoutCommand;
            AddNewItemCommand = addNewItemCommand;
            AddNewFriendCommand = addNewFriendCommand;

            var userPreferences = Resolver.Resolve<UserPreferences>();

            OptionAddFriend.IsVisible = userPreferences.IsAdministrator;
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
        private async void AddNewItem_Tapped(object sender, System.EventArgs e)
        {
            await CloseThisModal();
            AddNewItemCommand.Execute(null);
        }
        private async void AddNewFriend_Tapped(object sender, System.EventArgs e)
        {
            await CloseThisModal();
            AddNewFriendCommand.Execute(null);
        }
    }
}
