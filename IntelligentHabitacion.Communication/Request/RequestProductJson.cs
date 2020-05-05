﻿using IntelligentHabitacion.Communication.Response;

namespace IntelligentHabitacion.Communication.Request
{
    public class RequestProductJson
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Manufacturer { get; set; }
        public Type Type { get; set; }
    }
}
