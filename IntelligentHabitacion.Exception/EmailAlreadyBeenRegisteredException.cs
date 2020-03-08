using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
    public class EmailAlreadyBeenRegisteredException : IntelligentHabitacionException
    {
        public EmailAlreadyBeenRegisteredException() : base(ResourceTextException.EMAIL_ALREADYBEENREGISTERED)
        {
        }
    }
}
