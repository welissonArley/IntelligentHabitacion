using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
    public class PasswordEmptyException : IntelligentHabitacionException
    {
        public PasswordEmptyException() : base(ResourceTextException.PASSWORD_EMPTY)
        {
        }
    }
}
