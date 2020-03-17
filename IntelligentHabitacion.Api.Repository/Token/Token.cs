using IntelligentHabitacion.Api.Repository.Model;

namespace IntelligentHabitacion.Api.Repository.Token
{
    public class Token
    {
        public virtual long Id { get; set; }
        public virtual string Value { get; set; }
        public virtual User User { get; set; }
    }
}
