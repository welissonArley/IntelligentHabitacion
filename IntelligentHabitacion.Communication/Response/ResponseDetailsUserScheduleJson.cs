using System;
using System.Collections.Generic;

namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseDetailsUserScheduleJson
    {
        public ResponseDetailsUserScheduleJson()
        {
            Tasks = new List<ResponseTaskForTheMonthDetailsJson>();
        }

        public string Name { get; set; }
        public DateTime Month { get; set; }
        public string ProfileColor { get; set; }
        public List<ResponseTaskForTheMonthDetailsJson> Tasks { get; set; }
    }
}
