using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class AddressEmptyException : IntelligentHabitacionException
    {
        public AddressEmptyException() : base(ResourceTextException.ADDRESS_EMPTY)
        {
        }
    }
}
