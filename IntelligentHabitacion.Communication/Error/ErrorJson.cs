using System.Collections.Generic;

namespace IntelligentHabitacion.Communication.Error
{
    public class ErrorJson
    {
        public ErrorJson(string message)
        {
            Errors = new List<string> { message };
        }
        public ErrorJson(List<string> messages)
        {
            Errors = messages;
        }
        public List<string> Errors { get; set; }
    }
}
