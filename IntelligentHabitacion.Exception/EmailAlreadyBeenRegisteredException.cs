using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class EmailAlreadyBeenRegisteredException : IntelligentHabitacionException
    {
        public EmailAlreadyBeenRegisteredException() : base(ResourceTextException.EMAIL_ALREADYBEENREGISTERED)
        {
        }
    }
}
