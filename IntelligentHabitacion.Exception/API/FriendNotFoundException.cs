using IntelligentHabitacion.Exception.ExceptionsBase;

#pragma warning disable S3925
namespace IntelligentHabitacion.Exception.API
{
    public class FriendNotFoundException : IntelligentHabitacionException
    {
        public FriendNotFoundException() : base(ResourceTextException.FRIEND_NOT_FOUND)
        {

        }
    }
}
