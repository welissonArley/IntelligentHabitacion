﻿using IntelligentHabitacion.Exception.ExceptionsBase;
using System;

namespace IntelligentHabitacion.Exception.API
{
    public class InvalidDateException : IntelligentHabitacionException
    {
        public InvalidDateException(DateTime date) : base(string.Format(ResourceTextException.DATE_MUST_BE_LESS_THAN, date.ToShortDateString()))
        {

        }
    }
}
