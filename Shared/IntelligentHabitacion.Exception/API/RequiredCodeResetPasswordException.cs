using IntelligentHabitacion.Exception.ExceptionsBase;

#pragma warning disable S3925
namespace IntelligentHabitacion.Exception.API
{
    public class RequiredCodeResetPasswordException : IntelligentHabitacionException
    {
        public RequiredCodeResetPasswordException() : base(ResourceTextException.CODE_RESET_PASSWORD_REQUIRED)
        {

        }
    }
}
