using System;

namespace IntelligentHabitacion.Communication.Request
{
    public class RequestCalendarCleaningScheduleJson
    {
        public DateTime Month { get; set; }
        public string Room { get; set; }
    }
}
