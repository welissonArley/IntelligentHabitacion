using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
    public class PasswordIsNotSameConfirmationException : IntelligentHabitacionException
    {
        public PasswordIsNotSameConfirmationException() : base(ResourceTextException.PASSWORD_NOT_SAME_CONFIRMATION)
        {
        }
    }
}
