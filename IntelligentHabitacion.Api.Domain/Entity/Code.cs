using IntelligentHabitacion.Api.Domain.Enums;
using IntelligentHabitacion.Api.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntelligentHabitacion.Api.Domain.Entity
{
    [Table("Code")]
    public class Code : EntityBase
    {
        public string Value { get; set; }
        public CodeType Type { get; set; }
        public long UserId { get; set; }
    }
}
