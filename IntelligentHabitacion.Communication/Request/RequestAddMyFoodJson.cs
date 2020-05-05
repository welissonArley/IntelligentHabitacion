using System;

namespace IntelligentHabitacion.Communication.Request
{
    public class RequestAddMyFoodJson : RequestProductJson
    {
        public DateTime? DueDate { get; set; }
    }
}
