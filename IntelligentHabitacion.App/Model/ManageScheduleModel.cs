using System.Collections.ObjectModel;
using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class ManageScheduleModel : ObservableObject
    {
        public ObservableCollection<RoomScheduleModel> RoomsAvaliables { get; set; }
        public ObservableCollection<AllFriendsGroup> UserTasks { get; set; }
    }
}
