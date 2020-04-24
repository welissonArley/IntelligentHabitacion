using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Useful;
using IntelligentHabitacion.App.View.Modal;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel.Friends
{
    public class FriendDetailsViewModel : BaseViewModel
    {
        public ICommand MakePhonecallCommand { protected set; get; }
        public ICommand MenuOptionsCommand { protected set; get; }

        public FriendModel Model { get; set; }

        public FriendDetailsViewModel()
        {
            MakePhonecallCommand = new Command(async (value) =>
            {
                await MakeCall(value.ToString());
            });
            MenuOptionsCommand = new Command(async () =>
            {
                await ShowAdministratorOptions();
            });
        }

        private async Task MakeCall(string number)
        {
            await ShowLoading();
            Phonecall.Make(number);
            HideLoading();
        }

        private async Task ShowAdministratorOptions()
        {
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PushPopupAsync(new MenuOptionsAdministratorFriendDetailModal());
        }
    }
}
