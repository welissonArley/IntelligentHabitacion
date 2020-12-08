using System;
using System.Collections.Generic;

namespace IntelligentHabitacion.App.Model
{
    public class ScheduleUserModel
    {
        public string Id { get; set; }
        public string User { get; set; }
        public DateTime Month { get; set; }
        public List<ScheduleModel> Schedules { get; set; }
    }

    public class ScheduleModel
    {
        public DateTime Date { get; set; }
        public string Room { get; set; }
        public sbyte RatingStars { get; set; }
        public bool CanBeRate { get; set; }
    }
}
