using IntelligentHabitacion.Exception.ExceptionsBase;

#pragma warning disable S3925
namespace IntelligentHabitacion.Exception.API
{
    public class UserIsPartOfAHomeException : IntelligentHabitacionException
    {
        public UserIsPartOfAHomeException() : base(ResourceTextException.USER_IS_PART_OF_A_HOME)
        {

        }
    }
}
