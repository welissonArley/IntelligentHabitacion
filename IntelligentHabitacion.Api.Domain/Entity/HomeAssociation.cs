using IntelligentHabitacion.Api.Domain.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntelligentHabitacion.Api.Domain.Entity
{
    [Table("HomeAssociation")]
    public class HomeAssociation : EntityBase
    {
        [ForeignKey("HomeId")]
        public Home Home { get; set; }
        public long HomeId { get; set; }
        public DateTime JoinedOn { get; set; }
        public DateTime? ExitOn { get; set; }
        public decimal MonthlyRent { get; set; }
    }
}
