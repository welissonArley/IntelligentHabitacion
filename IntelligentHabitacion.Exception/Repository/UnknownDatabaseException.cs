using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception.Repository
{
    public class UnknownDatabaseException : IntelligentHabitacionException
    {
        public UnknownDatabaseException() : base(ResourceTextException.UNKNOWNDATABASE)
        {
        }
    }
}
