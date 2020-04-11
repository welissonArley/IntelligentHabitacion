using System.Collections.Generic;

namespace IntelligentHabitacion.Communication.Error
{
    public enum ErrorCode
    {
        TokenExpired = 0,
        Error = 1
    }

    public class ErrorJson
    {
        public ErrorCode ErrorCode = ErrorCode.Error;
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
