using IntelligentHabitacion.Exception.ExceptionsBase;

#pragma warning disable S3925
namespace IntelligentHabitacion.Exception.API
{
    public class UserAlreadyRateTaskException : IntelligentHabitacionException
    {
        public UserAlreadyRateTaskException() : base(ResourceTextException.YOU_ALREADY_RATE_THIS_TASK)
        {

        }
    }
}
