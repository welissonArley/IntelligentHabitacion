using System;
using System.Collections.Generic;

namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseMyTasksCleaningCleanHouseJson
    {
        public DateTime Month { get; set; }
        public IList<ResponseTasksForTheMonthJson> Tasks { get; set; }
    }
}
