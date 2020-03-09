namespace IntelligentHabitacion.Exception.ExceptionsBase
{
#pragma warning disable S3925
    public class InvalidLoginException : IntelligentHabitacionException
    {
        public InvalidLoginException() : base(ResourceTextException.USER_OR_PASSWORD_INVALID)
        {
        }
    }
}
