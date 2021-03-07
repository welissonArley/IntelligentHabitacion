using System;
using System.Collections.Generic;

namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseRateTaskJson
    {
        public string CleanedBy { get; set; }
        public string Room { get; set; }
        public DateTime CleanedAt { get; set; }
        public List<ResponseRateJson> Rates { get; set; }
    }
}
