using IntelligentHabitacion.Communication.Enums;

namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseNeedActionJson
    {
        public string Message { get; set; }
        public NeedActionEnum Action { get; set; }
    }
}
