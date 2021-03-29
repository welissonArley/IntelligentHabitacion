using IntelligentHabitacion.Communication.Enums;

namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseTasksJson
    {
        public ResponseTasksJson()
        {
            CreateSchedule = new ResponseCreateScheduleCleaningHouseJson();
        }

        public NeedAction Action { get; set; }
        public string Message { get; set; }
        public ResponseCreateScheduleCleaningHouseJson CreateSchedule { get; set; }
    }
}
