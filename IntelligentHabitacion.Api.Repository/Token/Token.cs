using System.ComponentModel.DataAnnotations.Schema;

namespace IntelligentHabitacion.Api.Repository.Token
{
    [Table("Token")]
    public class Token
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public long UserId { get; set; }
    }
}
