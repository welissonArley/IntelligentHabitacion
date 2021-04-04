namespace IntelligentHabitacion.Api.Domain.Dto
{
    public class CleaningScheduleHistoryOfTheDayDto
    {
        public long Id { get; set; }
        public string User { get; set; }
        public int AverageRate { get; set; }
        public bool CanRate { get; set; }
    }
}
