using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
    public class EmailInvalidException : IntelligentHabitacionException
    {
        public EmailInvalidException() : base(ResourceTextException.EMAIL_INVALID)
        {
        }
    }
}
