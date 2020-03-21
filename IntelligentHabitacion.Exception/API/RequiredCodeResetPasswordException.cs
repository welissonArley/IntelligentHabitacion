using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception.API
{
    public class RequiredCodeResetPasswordException : IntelligentHabitacionException
    {
        public RequiredCodeResetPasswordException() : base(ResourceTextException.INVALID_USER)
        {

        }
    }
}
