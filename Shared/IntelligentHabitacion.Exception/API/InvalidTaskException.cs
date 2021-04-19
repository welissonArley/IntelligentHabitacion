using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception.API
{
#pragma warning disable S3925
    public class InvalidTaskException : IntelligentHabitacionException
    {
        public InvalidTaskException() : base(ResourceTextException.INVALID_TASK)
        {

        }
    }
}
