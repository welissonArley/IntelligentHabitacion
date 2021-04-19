using System;
using System.Collections.Generic;

namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseFriendJson
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<ResponsePhonenumberJson> Phonenumbers { get; set; }
        public List<ResponseEmergencyContactJson> EmergencyContacts { get; set; }
        public string ProfileColor { get; set; }
        public DateTime JoinedOn { get; set; }
        public string DescriptionDateJoined { get; set; }
    }
}
