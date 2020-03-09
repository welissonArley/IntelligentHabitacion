using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class NumberEmptyException : IntelligentHabitacionException
    {
        public NumberEmptyException() : base(ResourceTextException.NUMBER_EMPTY)
        {
        }
    }
}
