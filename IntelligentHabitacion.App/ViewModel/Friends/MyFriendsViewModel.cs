using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.Useful;
using IntelligentHabitacion.App.View.Modal;
using Rg.Plugins.Popup.Extensions;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel.Friends
{
    public class MyFriendsViewModel : BaseViewModel
    {
        public ICommand SearchTextChangedCommand { protected set; get; }
        public ICommand MakePhonecallCommand { protected set; get; }
        public ICommand DetailFriendCommand { protected set; get; }

        private readonly ObservableCollection<FriendModel> _friendsList;
        public ObservableCollection<FriendModel> FriendsList { get; set; }

        public MyFriendsViewModel(IHomeRule homeRule)
        {
            FriendsList = new ObservableCollection<FriendModel>(Task.Run(async () => await homeRule.GetHouseFriends()).Result);
            _friendsList = FriendsList;
            SearchTextChangedCommand = new Command((value) =>
            {
                OnSearchTextChanged((string)value);
            });
            MakePhonecallCommand = new Command(async (value) =>
            {
                await MakePhonecall((FriendModel)value);
            });
            DetailFriendCommand = new Command(async (value) =>
            {
                await OnDetailFriend((FriendModel)value);
            });
        }

        private void OnSearchTextChanged(string value)
        {
            FriendsList = new ObservableCollection<FriendModel>(_friendsList.Where(c => c.Name.ToUpper().Contains(value.ToUpper())).ToList());

            OnPropertyChanged(new PropertyChangedEventArgs("FriendsList"));
        }
        private async Task MakePhonecall(FriendModel friend)
        {
            if (string.IsNullOrWhiteSpace(friend.Phonenumber2))
                await MakeCall(friend.Phonenumber1);
            else
            {
                var navigation = Resolver.Resolve<INavigation>();
                await navigation.PushPopupAsync(new ChoosePhonenumberModal(friend.Name, friend.Phonenumber1, friend.Phonenumber2, friend.ProfileColor, MakeCall));
            }
        }
        private async Task OnDetailFriend(FriendModel friend)
        {
            await ShowLoading();
            await Navigation.PushAsync<FriendDetailsViewModel>((viewModel, page) => viewModel.Model = friend);
            HideLoading();
        }

        private async Task MakeCall(string number)
        {
            await ShowLoading();
            Phonecall.Make(number);
            HideLoading();
        }
    }
}
