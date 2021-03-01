using System.ComponentModel.DataAnnotations.Schema;

namespace IntelligentHabitacion.Api.Domain.Entity
{
    [Table("CleaningRatingUser")]
    public class CleaningRatingUser
    {
        public long UserId { get; set; }
        public long CleaningTasksCompletedId { get; set; }
    }
}
