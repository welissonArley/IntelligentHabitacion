using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
    public class EmailEmptyException : IntelligentHabitacionException
    {
        public EmailEmptyException() : base(ResourceTextException.EMAIL_EMPTY)
        {
        }
    }
}
