using IntelligentHabitacion.Exception.ExceptionsBase;

#pragma warning disable S3925
namespace IntelligentHabitacion.Exception.API
{
    public class ExpiredCodeException : IntelligentHabitacionException
    {
        public ExpiredCodeException() : base(ResourceTextException.EXPIRED_CODE)
        {

        }
    }
}
