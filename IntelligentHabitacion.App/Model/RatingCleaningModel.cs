using System;
using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class RatingCleaningModel : ObservableObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
        public DateTime Date { get; set; }
        public int RatingStars { get; set; }
        public string Feedback { get; set; }
    }
}
