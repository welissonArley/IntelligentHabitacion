using IntelligentHabitacion.Exception.ExceptionsBase;

#pragma warning disable S3925
namespace IntelligentHabitacion.Exception.API
{
    public class UserIsNotPartOfAHomeException : IntelligentHabitacionException
    {
        public UserIsNotPartOfAHomeException() : base(ResourceTextException.USER_IS_NOT_PART_OF_A_HOME)
        {

        }
    }
}
