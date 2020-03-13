using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class DegreeKinshipEmptyException : IntelligentHabitacionException
    {
        public DegreeKinshipEmptyException() : base(ResourceTextException.FAMILYRELATIONSHIP_EMPTY)
        {
        }
    }
}
