using IntelligentHabitacion.Exception.ExceptionsBase;

#pragma warning disable S3925
namespace IntelligentHabitacion.Exception.API
{
    public class CodeOrPasswordInvalidException : IntelligentHabitacionException
    {
        public CodeOrPasswordInvalidException() : base(ResourceTextException.CODE_OR_PASSWORD_INVALID)
        {

        }
    }
}
