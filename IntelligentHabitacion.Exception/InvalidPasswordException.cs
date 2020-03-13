﻿using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class InvalidPasswordException : IntelligentHabitacionException
    {
        public InvalidPasswordException() : base(ResourceTextException.INVALID_PASSWORD)
        {
        }
    }
}
