using System.Collections.Generic;

namespace IntelligentHabitacion.Api.Domain.ValueObjects
{
    public class MessageBodyOneSignal
    {
        public string app_id { get; set; }
        public List<string> include_player_ids { get; set; }
        public Dictionary<string, string> contents { get; set; }
        public Dictionary<string, string> headings { get; set; }
        public string delayed_option { get; set; }
        public string delivery_time_of_day { get; set; }
    }
}
