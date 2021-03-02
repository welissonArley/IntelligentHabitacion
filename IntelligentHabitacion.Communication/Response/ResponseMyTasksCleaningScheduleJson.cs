using System;
using System.Collections.Generic;

namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseMyTasksCleaningScheduleJson
    {
        public DateTime Month { get; set; }
        public IList<ResponseTasksForTheMonthJson> Tasks { get; set; }
    }
}
