using IntelligentHabitacion.Api.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntelligentHabitacion.Api.Domain.Entity
{
    [Table("EmergencyContact")]
    public class EmergencyContact : EntityBase
    {
        public string Name { get; set; }
        public string Relationship { get; set; }
        public string Phonenumber { get; set; }
        public long UserId { get; set; }
    }
}
