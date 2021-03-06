using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class DetailsUserScheduleModel : ObservableObject
    {
        public string Name { get; set; }
        public DateTime Month { get; set; }
        public string ProfileColor { get; set; }
        public ObservableCollection<TaskForTheMonthDetailsGroup> Tasks { get; set; }
    }

    public class TaskForTheMonthDetailsGroup : ObservableCollection<TaskForTheMonthDetails>
    {
        public DateTime Date { get; set; }

        public TaskForTheMonthDetailsGroup(DateTime date, IEnumerable<TaskForTheMonthDetails> tasks) : base(new ObservableCollection<TaskForTheMonthDetails>(tasks))
        {
            Date = date;
        }
    }

    public class TaskForTheMonthDetails : ObservableObject
    {
        public string Id { get; set; }
        public string Room { get; set; }
        public int RatingStars { get; set; }
        public bool CanBeRate { get; set; }
    }
}
