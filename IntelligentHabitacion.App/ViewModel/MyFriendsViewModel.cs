using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel
{
    public class MyFriendsViewModel : BaseViewModel
    {
        public ICommand SearchTextChangedCommand { protected set; get; }

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
        }

        private void OnSearchTextChanged(string value)
        {
            FriendsList = new ObservableCollection<FriendModel>(_friendsList.Where(c => c.Name.ToUpper().Contains(value.ToUpper())).ToList());

            OnPropertyChanged(new PropertyChangedEventArgs("FriendsList"));
        }
    }
}
