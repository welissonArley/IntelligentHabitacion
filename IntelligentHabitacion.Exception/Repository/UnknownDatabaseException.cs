using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception.Repository
{
#pragma warning disable S3925
    public class UnknownDatabaseException : IntelligentHabitacionException
    {
        public UnknownDatabaseException() : base(ResourceTextException.UNKNOWNDATABASE)
        {
        }
    }
}
