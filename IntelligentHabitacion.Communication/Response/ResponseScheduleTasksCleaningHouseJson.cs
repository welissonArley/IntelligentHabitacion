using System;
using System.Collections.Generic;

namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseScheduleTasksCleaningHouseJson
    {
        public ResponseScheduleTasksCleaningHouseJson()
        {
            Tasks = new List<ResponseTaskJson>();
        }

        public string ProfileColor { get; set; }
        public string Name { get; set; }
        public int AmountOfTasks { get; set; }
        
        public DateTime Date { get; set; }
        public IList<ResponseTaskJson> Tasks { get; set; }
    }
}
