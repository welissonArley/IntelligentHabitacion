using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
    public class NumberEmptyException : IntelligentHabitacionException
    {
        public NumberEmptyException() : base(ResourceTextException.NUMBER_EMPTY)
        {
        }
    }
}
