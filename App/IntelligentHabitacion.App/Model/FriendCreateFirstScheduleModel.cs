using System.Collections.ObjectModel;
using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class FriendCreateFirstScheduleModel : ObservableObject
    {
        public FriendCreateFirstScheduleModel()
        {
            Tasks = new ObservableCollection<RoomModel>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string ProfileColor { get; set; }
        public ObservableCollection<RoomModel> Tasks { get; set; }
    }
}
