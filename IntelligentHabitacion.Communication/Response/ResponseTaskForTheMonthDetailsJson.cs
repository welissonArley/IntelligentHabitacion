using System;

namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseTaskForTheMonthDetailsJson
    {
        public DateTime Date { get; set; }
        public string Id { get; set; }
        public bool CanBeRate { get; set; }
        public int AverageRating { get; set; }
        public string Room { get; set; }
    }
}
