using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception.API
{
    public class CurrentPasswordException : IntelligentHabitacionException
    {
        public CurrentPasswordException() : base(ResourceTextException.CURRENT_PASSWORD_INVALID)
        {

        }
    }
}
