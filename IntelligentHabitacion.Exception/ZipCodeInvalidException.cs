﻿using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class ZipCodeInvalidException : IntelligentHabitacionException
    {
        public ZipCodeInvalidException() : base(ResourceTextException.ZIPCODE_INVALID)
        {
        }
    }
}
