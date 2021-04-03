using System;
using System.Collections.Generic;

namespace IntelligentHabitacion.App.Model
{
    public class CleaningScheduleCalendarModel
    {
        public DateTime Date { get; set; }
        public IList<CleaningScheduleCalendarDayInfoModel> CleanedDays { get; set; }
    }
}
