using System;
using System.Collections.ObjectModel;

namespace IntelligentHabitacion.App.Model
{
    public class MyTasksCleanHouseModel
    {
        public string Name { get; set; }
        public DateTime Month { get; set; }
        public ObservableCollection<TasksForTheMonth> Tasks { get; set; }
    }
}
