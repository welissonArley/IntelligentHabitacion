using System;
using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class TasksForTheMonth : ObservableObject
    {
        public string Id { get; set; }
        public string Room { get; set; }
        public int CleaningRecords { get; set; }
        public DateTime? LastRecord { get; set; }
    }
}
