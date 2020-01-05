using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
    public class InvalidPasswordException : IntelligentHabitacionException
    {
        public InvalidPasswordException() : base(ResourceTextException.INVALID_PASSWORD)
        {
        }
    }
}
