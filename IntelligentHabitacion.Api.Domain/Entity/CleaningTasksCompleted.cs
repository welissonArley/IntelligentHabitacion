using IntelligentHabitacion.Api.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace IntelligentHabitacion.Api.Domain.Entity
{
    [Table("CleaningTasksCompleted")]
    public class CleaningTasksCompleted : EntityBase
    {
        [NotMapped]
        public int AverageRating { get => Ratings.Any() ? (int)Math.Round(Ratings.Average(c => c.Rating)) : -1; }
        public long CleaningScheduleId { get; set; }
        public IList<CleaningRating> Ratings { get; set; }
    }
}
