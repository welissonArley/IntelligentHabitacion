using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class CurrentPasswordEmptyException : IntelligentHabitacionException
    {
        public CurrentPasswordEmptyException() : base(ResourceTextException.CURRENT_PASSWORD_EMPTY)
        {
        }
    }
}
