using System.Collections.Generic;

namespace IntelligentHabitacion.Api.Repository.Model
{
    public class EmergencyContact : ModelBase
    {
        public virtual string Name { get; set; }
        public virtual string DegreeOfKinship { get; set; }
        public virtual ICollection<Phonenumber> Phonenumbers { get; set; }
    }
}
