using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception.API
{
    public class CodeOrPasswordInvalidException : IntelligentHabitacionException
    {
        public CodeOrPasswordInvalidException() : base(ResourceTextException.CODE_OR_PASSWORD_INVALID)
        {

        }
    }
}
