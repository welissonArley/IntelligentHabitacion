using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class PasswordIsNotSameConfirmationException : IntelligentHabitacionException
    {
        public PasswordIsNotSameConfirmationException() : base(ResourceTextException.PASSWORD_NOT_SAME_CONFIRMATION)
        {
        }
    }
}
