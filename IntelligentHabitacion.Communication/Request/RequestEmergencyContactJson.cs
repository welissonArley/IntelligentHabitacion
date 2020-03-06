using System.Collections.Generic;

namespace IntelligentHabitacion.Communication.Request
{
    public class RequestEmergencyContactJson
    {
        public RequestEmergencyContactJson()
        {
            Phonenumbers = new List<string>();
        }

        public string Name { get; set; }
        public string DegreeOfKinship { get; set; }
        public List<string> Phonenumbers { get; set; }
    }
}
