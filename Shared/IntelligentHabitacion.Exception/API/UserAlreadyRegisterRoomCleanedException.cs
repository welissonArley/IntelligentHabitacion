using IntelligentHabitacion.Exception.ExceptionsBase;

#pragma warning disable S3925
namespace IntelligentHabitacion.Exception.API
{
    public class UserAlreadyRegisterRoomCleanedException : IntelligentHabitacionException
    {
        public UserAlreadyRegisterRoomCleanedException() : base(ResourceTextException.THERE_IS_CLEAN_ROOM_RECORD_THIS_DATE)
        {

        }
    }
}
