using IntelligentHabitacion.Communication.Enums;
using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class TasksModel : ObservableObject
    {
        public NeedAction Action { get; set; }
        public string Message { get; set; }
        public CreateScheduleCleaningHouseModel CreateSchedule { get; set; }
    }
}
