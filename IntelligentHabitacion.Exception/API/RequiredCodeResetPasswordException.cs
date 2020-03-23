using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception.API
{
    public class RequiredCodeResetPasswordException : IntelligentHabitacionException
    {
        public RequiredCodeResetPasswordException() : base(ResourceTextException.CODE_RESET_PASSWORD_REQUIRED)
        {

        }
    }
}
