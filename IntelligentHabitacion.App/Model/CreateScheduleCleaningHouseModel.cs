using System.Collections.Generic;
using System.Collections.ObjectModel;
using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class CreateScheduleCleaningHouseModel : ObservableObject
    {
        public IList<RoomModel> Rooms { get; set; }
        public ObservableCollection<FriendCreateFirstScheduleModel> Friends { get; set; }
    }
}
