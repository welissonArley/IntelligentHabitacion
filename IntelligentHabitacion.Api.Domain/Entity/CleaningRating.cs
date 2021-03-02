using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntelligentHabitacion.Api.Domain.Entity
{
    [Table("CleaningRating")]
    public class CleaningRating
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Rating { get; set; }
        public string FeedBack { get; set; }
        [ForeignKey("CleaningTasksCompletedId")]
        public CleaningTasksCompleted CleaningTasksCompleted { get; set; }
        public long CleaningTasksCompletedId { get; set; }
    }
}
