using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class NeighborhoodEmptyException : IntelligentHabitacionException
    {
        public NeighborhoodEmptyException() : base(ResourceTextException.NEIGHBORHOOD_EMPTY)
        {
        }
    }
}
