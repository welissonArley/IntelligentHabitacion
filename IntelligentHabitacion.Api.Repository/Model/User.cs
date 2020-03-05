using System.Collections.Generic;

namespace IntelligentHabitacion.Api.Repository.Model
{
    public class User : ModelBase
    {
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual ICollection<Phonenumber> Phonenumbers { get; set; }
        public virtual ICollection<EmergencyContact> EmergecyContacts { get; set; }
    }
}
