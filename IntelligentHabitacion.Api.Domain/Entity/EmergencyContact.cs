using IntelligentHabitacion.Api.Domain.ValueObjects;

namespace IntelligentHabitacion.Api.Domain.Entity
{
    public class EmergencyContact : EntityBase
    {
        public string Name { get; set; }
        public string Relationship { get; set; }
        public string Phonenumber { get; set; }
    }
}
