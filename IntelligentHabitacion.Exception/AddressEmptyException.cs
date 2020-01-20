using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
    public class AddressEmptyException : IntelligentHabitacionException
    {
        public AddressEmptyException() : base(ResourceTextException.ADDRESS_EMPTY)
        {
        }
    }
}
