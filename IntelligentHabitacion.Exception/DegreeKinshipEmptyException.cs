using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
    public class DegreeKinshipEmptyException : IntelligentHabitacionException
    {
        public DegreeKinshipEmptyException() : base(ResourceTextException.FAMILYRELATIONSHIP_EMPTY)
        {
        }
    }
}
