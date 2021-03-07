using IntelligentHabitacion.Exception.ExceptionsBase;

#pragma warning disable S3925
namespace IntelligentHabitacion.Exception.API
{
    public class InvalidRatingException : IntelligentHabitacionException
    {
        public InvalidRatingException() : base(ResourceTextException.RATE_SCORE_BETWEEN_ZERO_FIVE)
        {

        }
    }
}
