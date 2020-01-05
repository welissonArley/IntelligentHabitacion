using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
    public class NameEmptyException : IntelligentHabitacionException
    {
        public NameEmptyException() : base(ResourceTextException.NAME_EMPTY)
        {
        }
    }
}
