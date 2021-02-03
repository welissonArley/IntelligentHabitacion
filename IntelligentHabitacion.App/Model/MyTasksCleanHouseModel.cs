using System;
using System.Collections.ObjectModel;

namespace IntelligentHabitacion.App.Model
{
    public class MyTasksCleanHouseModel
    {
        public string Name { get; set; }
        public DateTime Month { get; set; }
        public ObservableCollection<MyTasksForTheMonth> Tasks { get; set; }
    }

    public class MyTasksForTheMonth
    {
        public string Room { get; set; }
        public int CleaningRecords { get; set; }
        public DateTime? LastRecord { get; set; }
    }
}
