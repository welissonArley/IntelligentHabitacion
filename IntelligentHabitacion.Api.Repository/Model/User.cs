using System.Collections.Generic;

namespace IntelligentHabitacion.Api.Repository.Model
{
    public class User : ModelBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Phonenumber> Phonenumbers { get; set; }
        public List<EmergencyContact> EmergecyContacts { get; set; }
    }
}
