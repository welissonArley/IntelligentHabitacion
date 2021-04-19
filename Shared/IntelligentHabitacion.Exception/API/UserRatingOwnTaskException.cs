using IntelligentHabitacion.Exception.ExceptionsBase;

#pragma warning disable S3925
namespace IntelligentHabitacion.Exception.API
{
    public class UserRatingOwnTaskException : IntelligentHabitacionException
    {
        public UserRatingOwnTaskException() : base(ResourceTextException.YOU_CANNOT_RATE_YOUR_OWN_TASK)
        {

        }
    }
}
