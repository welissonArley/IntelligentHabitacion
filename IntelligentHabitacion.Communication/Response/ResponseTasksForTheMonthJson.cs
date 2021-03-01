using System;

namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseTasksForTheMonthJson
    {
        public string Id { get; set; }
        public string Room { get; set; }
        public int CleaningRecords { get; set; }
        public DateTime? LastRecord { get; set; }
    }
}
