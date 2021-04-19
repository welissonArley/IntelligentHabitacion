using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class EmailEmptyException : IntelligentHabitacionException
    {
        public EmailEmptyException() : base(ResourceTextException.EMAIL_EMPTY)
        {
        }
    }
}
