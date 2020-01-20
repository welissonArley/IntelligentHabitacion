using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
    public class NeighborhoodEmptyException : IntelligentHabitacionException
    {
        public NeighborhoodEmptyException() : base(ResourceTextException.NEIGHBORHOOD_EMPTY)
        {
        }
    }
}
