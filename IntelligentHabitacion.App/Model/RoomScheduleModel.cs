using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class RoomScheduleModel : ObservableObject
    {
        public string Room { get; set; }
        public bool Assigned { get; set; }
    }
}
