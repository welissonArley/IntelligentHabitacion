using IntelligentHabitacion.Exception.ExceptionsBase;

#pragma warning disable S3925
namespace IntelligentHabitacion.Exception.API
{
    public class CurrentPasswordException : IntelligentHabitacionException
    {
        public CurrentPasswordException() : base(ResourceTextException.CURRENT_PASSWORD_INVALID)
        {

        }
    }
}
