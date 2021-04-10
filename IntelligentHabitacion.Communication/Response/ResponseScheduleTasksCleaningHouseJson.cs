﻿using System;
using System.Collections.Generic;

namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseScheduleTasksCleaningHouseJson
    {
        public ResponseScheduleTasksCleaningHouseJson()
        {
            Tasks = new List<ResponseTaskJson>();
            AvaliableUsersToAssign = new List<ResponseUserSimplifiedJson>();
        }

        public string ProfileColor { get; set; }
        public string Name { get; set; }
        public int AmountOfTasks { get; set; }
        
        public DateTime Date { get; set; }
        public IList<ResponseTaskJson> Tasks { get; set; }
        public IList<ResponseUserSimplifiedJson> AvaliableUsersToAssign { get; set; }
    }
}
