using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
    public class ZipCodeEmptyException : IntelligentHabitacionException
    {
        public ZipCodeEmptyException() : base(ResourceTextException.ZIPCODE_EMPTY)
        {
        }
    }
}
