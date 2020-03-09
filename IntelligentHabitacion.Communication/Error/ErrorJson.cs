using System.Collections.Generic;

namespace IntelligentHabitacion.Communication.Error
{
    public class ErrorJson
    {
        public List<string> Errors { get; set; }

        public ErrorJson() { /* Use only for JsonConvert.DeserializeObject<ErrorJson> */ }
        public ErrorJson(string message)
        {
            Errors = new List<string> { message };
        }
        public ErrorJson(List<string> messages)
        {
            Errors = messages;
        }
    }
}
