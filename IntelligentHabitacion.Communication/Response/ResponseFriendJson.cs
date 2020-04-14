using System.Collections.Generic;

namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseFriendJson
    {
        public string Name { get; set; }
        public List<ResponsePhonenumberJson> Phonenumbers { get; set; }
        public List<ResponseEmergencyContactJson> EmergencyContactc { get; set; }
    }
}
