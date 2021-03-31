using IntelligentHabitacion.Exception.ExceptionsBase;

#pragma warning disable S3925
namespace IntelligentHabitacion.Exception.API
{
    public class UserCannotRegisterRoomThisDateCleanedException : IntelligentHabitacionException
    {
        public UserCannotRegisterRoomThisDateCleanedException() : base(ResourceTextException.RECORD_CLEANING_ROOM_INVALID_DATE)
        {

        }
    }
}
