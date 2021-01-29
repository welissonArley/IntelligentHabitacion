using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.Model
{
    public class MyTasksModel
    {
        public string Name { get; set; }
        public DateTime Month { get; set; }
        public string ProfileColor { get; set; }
        public ObservableCollection<MyTask> MyTasksForTheMonth { get; set; }
        public ObservableCollection<Historic> Historics { get; set; }
    }

    public class Historic : ObservableCollection<MyTaskHistoric>
    {
        public DateTime Date { get; set; }

        public Historic(DateTime date, ObservableCollection<MyTaskHistoric> historics) : base(historics)
        {
            Date = date;
        }
    }

    public class MyTaskHistoric
    {
        public string Room { get; set; }
        public sbyte RatingStars { get; set; }
    }
    public class MyTask
    {
        public string Room { get; set; }
        public string Note { get; set; }
    }
}
