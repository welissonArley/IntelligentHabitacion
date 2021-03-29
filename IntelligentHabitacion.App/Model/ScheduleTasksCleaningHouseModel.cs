using System;
using System.Collections.ObjectModel;
using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class ScheduleTasksCleaningHouseModel : ObservableObject
    {
        public string ProfileColor { get; set; }
        public string Name { get; set; }
        public int AmountOfTasks { get; set; }

        public DateTime Date { get; set; }
        public ObservableCollection<TaskModel> Tasks { get; set; }
    }
}
