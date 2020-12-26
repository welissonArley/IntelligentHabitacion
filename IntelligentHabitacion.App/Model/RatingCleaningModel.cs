using System;

namespace IntelligentHabitacion.App.Model
{
    public class RatingCleaningModel
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Room { get; set; }
        public sbyte RatingStars { get; set; }
    }
}
