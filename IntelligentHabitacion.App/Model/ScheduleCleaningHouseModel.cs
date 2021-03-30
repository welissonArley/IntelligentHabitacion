﻿using IntelligentHabitacion.Communication.Enums;
using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class ScheduleCleaningHouseModel : ObservableObject
    {
        public ScheduleCleaningHouseModel()
        {
            CreateSchedule = new CreateScheduleCleaningHouseModel();
            Schedule = new ScheduleTasksCleaningHouseModel();
        }

        public NeedAction Action { get; set; }
        public string Message { get; set; }
        public CreateScheduleCleaningHouseModel CreateSchedule { get; set; }
        public ScheduleTasksCleaningHouseModel Schedule { get; set; }
    }
}
