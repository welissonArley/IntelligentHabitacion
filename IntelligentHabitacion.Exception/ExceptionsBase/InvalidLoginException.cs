namespace IntelligentHabitacion.Exception.ExceptionsBase
{
    public class InvalidLoginException : IntelligentHabitacionException
    {
        public InvalidLoginException() : base(ResourceTextException.USER_OR_PASSWORD_INVALID)
        {
        }
    }
}
