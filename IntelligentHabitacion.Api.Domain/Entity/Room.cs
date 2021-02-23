using IntelligentHabitacion.Api.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntelligentHabitacion.Api.Domain.Entity
{
    public class Room : EntityBase
    {
        public string Name { get; set; }
        [ForeignKey("HomeId")]
        public Home Home { get; set; }
        public long HomeId { get; set; }
    }
}
