using System;
using System.Collections.ObjectModel;
using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class ScheduleTasksCleaningHouseModel : ObservableObject
    {
        public DateTime Date { get; set; }
        public ObservableCollection<TaskModel> Tasks { get; set; }
    }
}
