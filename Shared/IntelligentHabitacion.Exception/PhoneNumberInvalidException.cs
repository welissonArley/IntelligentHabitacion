using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class PhoneNumberInvalidException : IntelligentHabitacionException
    {
        public PhoneNumberInvalidException() : base(ResourceTextException.PHONENUMBER_INVALID)
        {
        }
    }
}
