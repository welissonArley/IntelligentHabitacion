using System;

namespace IntelligentHabitacion.Api.Domain.Dto
{
    public class MyTasksCleaningScheduleDto
    {
        public long Id { get; set; }
        public string Room { get; set; }
        public int CleaningRecords { get; set; }
        public DateTime? LastRecord { get; set; }
    }
}
