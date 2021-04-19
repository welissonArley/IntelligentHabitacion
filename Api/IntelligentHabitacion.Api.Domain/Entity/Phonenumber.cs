using IntelligentHabitacion.Api.Domain.ValueObjects;

namespace IntelligentHabitacion.Api.Domain.Entity
{
    public class Phonenumber : EntityBase
    {
        public string Number { get; set; }
        public long UserId { get; set; }
    }
}
