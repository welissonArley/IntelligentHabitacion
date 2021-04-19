using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class RelationshipToEmptyException : IntelligentHabitacionException
    {
        public RelationshipToEmptyException() : base(ResourceTextException.RELATIONSHIPTO_EMPTY)
        {
        }
    }
}
