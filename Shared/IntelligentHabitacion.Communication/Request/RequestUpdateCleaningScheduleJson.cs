using System.Collections.Generic;

namespace IntelligentHabitacion.Communication.Request
{
    public class RequestUpdateCleaningScheduleJson
    {
        public string UserId { get; set; }
        public IList<string> Rooms { get; set; }
    }
}
