using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception.API
{
#pragma warning disable S3925
    public class InvalidUserException : IntelligentHabitacionException
    {
        public InvalidUserException() : base(ResourceTextException.INVALID_USER)
        {

        }
    }
}
