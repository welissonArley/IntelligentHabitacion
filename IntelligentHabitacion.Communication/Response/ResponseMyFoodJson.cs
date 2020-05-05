using System;

namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseMyFoodJson : ResponseProductJson
    {
        public DateTime? DueDate { get; set; }
    }
}
