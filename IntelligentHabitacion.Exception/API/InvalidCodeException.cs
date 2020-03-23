using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception.API
{
    public class InvalidCodeException : IntelligentHabitacionException
    {
        public InvalidCodeException() : base(ResourceTextException.CODE_INVALID)
        {

        }
    }
}
