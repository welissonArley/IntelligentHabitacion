﻿using System.Collections.Generic;

namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseUserInformationsJson
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<ResponsePhonenumberJson> Phonenumbers { get; set; }
        public List<ResponseEmergencyContactJson> EmergencyContactc { get; set; }
    }
}