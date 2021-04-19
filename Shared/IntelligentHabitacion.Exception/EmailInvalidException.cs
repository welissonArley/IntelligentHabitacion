using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class EmailInvalidException : IntelligentHabitacionException
    {
        public EmailInvalidException() : base(ResourceTextException.EMAIL_INVALID)
        {
        }
    }
}
