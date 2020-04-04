using System;

#pragma warning disable S3925
namespace IntelligentHabitacion.Exception.ExceptionsBase
{
    public class ResponseException : SystemException
    {
        public string Token { get; set; }
        public object Exception { get; set; }
    }
}
