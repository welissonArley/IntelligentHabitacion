using System;
using System.Collections.Generic;

namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseCalendarCleaningScheduleJson
    {
        public DateTime Date { get; set; }
        public IList<ResponseCleaningScheduleCalendarDayInfoJson> CleanedDays { get; set; }
    }
}
