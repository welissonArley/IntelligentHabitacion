using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Useful;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.Friends
{
    public class FriendDetailsViewModel : BaseViewModel
    {
        public ICommand MakePhonecallCommand { protected set; get; }
        public FriendModel Model { get; set; }

        public FriendDetailsViewModel()
        {
            MakePhonecallCommand = new Command(async (value) =>
            {
                await MakeCall(value.ToString());
            });
        }

        private async Task MakeCall(string number)
        {
            await ShowLoading();
            Phonecall.Make(number);
            HideLoading();
        }
    }
}
