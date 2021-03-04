using System.Collections.Generic;

namespace IntelligentHabitacion.Api.Domain.Dto
{
    public class UpdateCleaningScheduleDto
    {
        public long UserId { get; set; }
        public IList<string> Rooms { get; set; }
    }
}
