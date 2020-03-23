using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception.API
{
    public class ExpiredCodeException : IntelligentHabitacionException
    {
        public ExpiredCodeException() : base(ResourceTextException.EXPIRED_CODE)
        {

        }
    }
}
