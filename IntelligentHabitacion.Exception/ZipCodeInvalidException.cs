using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
    public class ZipCodeInvalidException : IntelligentHabitacionException
    {
        public ZipCodeInvalidException() : base(ResourceTextException.ZIPCODE_INVALID)
        {
        }
    }
}
