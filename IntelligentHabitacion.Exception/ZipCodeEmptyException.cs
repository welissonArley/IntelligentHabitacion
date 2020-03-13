using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class ZipCodeEmptyException : IntelligentHabitacionException
    {
        public ZipCodeEmptyException() : base(ResourceTextException.ZIPCODE_EMPTY)
        {
        }
    }
}
