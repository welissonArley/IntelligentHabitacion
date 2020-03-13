using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class CodeEmptyException : IntelligentHabitacionException
    {
        public CodeEmptyException() : base(ResourceTextException.CODE_EMPTY)
        {
        }
    }
}
