using System.Collections.Generic;

namespace IntelligentHabitacion.Api.Repository.Model
{
    public class EmergencyContact : ModelBase
    {
        public string Name { get; set; }
        public string DegreeOfKinship { get; set; }
        public List<Phonenumber> Phonenumbers { get; set; }
    }
}
