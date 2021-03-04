using System;

namespace IntelligentHabitacion.App.Model
{
    public class TasksForTheMonth
    {
        public string Room { get; set; }
        public int CleaningRecords { get; set; }
        public DateTime? LastRecord { get; set; }
    }
}
