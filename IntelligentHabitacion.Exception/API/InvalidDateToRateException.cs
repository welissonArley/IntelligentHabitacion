using IntelligentHabitacion.Exception.ExceptionsBase;

#pragma warning disable S3925
namespace IntelligentHabitacion.Exception.API
{
    public class InvalidDateToRateException : IntelligentHabitacionException
    {
        public InvalidDateToRateException() : base(ResourceTextException.RATE_TASK_JUST_CURRENT_MONTHS_SCHEDULE)
        {

        }
    }
}
