using System.Collections.ObjectModel;

namespace IntelligentHabitacion.App.Model
{
    public class ManageScheduleModel
    {
        public ObservableCollection<RoomScheduleModel> RoomsAvaliables { get; set; }
        public ObservableCollection<AllFriendsGroup> UserTasks { get; set; }
    }
}
