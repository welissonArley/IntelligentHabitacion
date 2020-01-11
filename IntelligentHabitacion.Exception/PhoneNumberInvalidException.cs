using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
    public class PhoneNumberInvalidException : IntelligentHabitacionException
    {
        public PhoneNumberInvalidException() : base(ResourceTextException.PHONENUMBER_INVALID)
        {
        }
    }
}
