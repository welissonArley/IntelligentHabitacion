using System;
using System.Collections.ObjectModel;
using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class MyTasksCleanHouseModel : ObservableObject
    {
        public string Name { get; set; }
        public DateTime Month { get; set; }
        public ObservableCollection<TasksForTheMonth> Tasks { get; set; }
    }
}
