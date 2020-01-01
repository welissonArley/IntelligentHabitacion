using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
    public class CodeEmptyException : IntelligentHabitacionException
    {
        public CodeEmptyException() : base(ResourceTextException.CODE_EMPTY)
        {
        }
    }
}
