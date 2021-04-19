using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class PhoneNumberEmptyException : IntelligentHabitacionException
    {
        public PhoneNumberEmptyException() : base(ResourceTextException.PHONENUMBER_EMPTY)
        {
        }
    }
}
