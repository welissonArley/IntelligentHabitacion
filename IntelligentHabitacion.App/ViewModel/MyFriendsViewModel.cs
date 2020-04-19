using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.ViewModel
{
    public class MyFriendsViewModel : BaseViewModel
    {
        public ObservableCollection<FriendModel> FriendsList { get; set; }

        public MyFriendsViewModel(IHomeRule homeRule)
        {
            FriendsList = Task.Run(async () => await homeRule.GetHouseFriends()).Result;
        }
    }
}
