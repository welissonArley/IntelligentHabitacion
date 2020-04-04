using IntelligentHabitacion.Exception.ExceptionsBase;

#pragma warning disable S3925
namespace IntelligentHabitacion.Exception.API
{
    public class InvalidCodeException : IntelligentHabitacionException
    {
        public InvalidCodeException() : base(ResourceTextException.CODE_INVALID)
        {

        }
    }
}
