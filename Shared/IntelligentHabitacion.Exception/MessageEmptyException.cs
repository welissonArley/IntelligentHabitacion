using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class MessageEmptyException : IntelligentHabitacionException
    {
        public MessageEmptyException() : base(ResourceTextException.MESSAGE_EMPTY)
        {
        }
    }
}
