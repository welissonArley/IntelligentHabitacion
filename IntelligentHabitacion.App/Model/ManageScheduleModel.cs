using System.Collections.ObjectModel;

namespace IntelligentHabitacion.App.Model
{
    public class ManageScheduleModel
    {
        public ObservableCollection<RoomModel> RoomsAvaliables { get; set; }
        public ObservableCollection<AllFriendsGroup> UserTasks { get; set; }
    }
}
