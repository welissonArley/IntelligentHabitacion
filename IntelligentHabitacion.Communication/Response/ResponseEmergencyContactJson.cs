using System.Collections.Generic;

namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseEmergencyContactJson
    {
        public string Name { get; set; }
        public string DegreeOfKinship { get; set; }
        public List<ResponsePhonenumberJson> Phonenumbers { get; set; }
    }
}
