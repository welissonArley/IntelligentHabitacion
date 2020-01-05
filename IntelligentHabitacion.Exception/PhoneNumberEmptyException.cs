using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
    public class PhoneNumberEmptyException : IntelligentHabitacionException
    {
        public PhoneNumberEmptyException() : base(ResourceTextException.PHONENUMBER_EMPTY)
        {
        }
    }
}
