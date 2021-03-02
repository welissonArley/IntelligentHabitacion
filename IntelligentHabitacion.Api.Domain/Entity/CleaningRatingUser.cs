using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntelligentHabitacion.Api.Domain.Entity
{
    [Table("CleaningRatingUser")]
    public class CleaningRatingUser
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public long UserId { get; set; }
        public long CleaningTasksCompletedId { get; set; }
    }
}
