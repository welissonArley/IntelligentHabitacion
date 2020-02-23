using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
    public class CurrentPasswordEmptyException : IntelligentHabitacionException
    {
        public CurrentPasswordEmptyException() : base(ResourceTextException.CURRENT_PASSWORD_EMPTY)
        {
        }
    }
}
