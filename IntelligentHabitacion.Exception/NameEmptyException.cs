using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class NameEmptyException : IntelligentHabitacionException
    {
        public NameEmptyException() : base(ResourceTextException.NAME_EMPTY)
        {
        }
    }
}
