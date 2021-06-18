namespace Homuai.Exception.ExceptionsBase
{
    public class InvalidLoginException : HomuaiException
    {
        public InvalidLoginException() : base(ResourceTextException.USER_OR_PASSWORD_INVALID)
        {
        }
    }
}
