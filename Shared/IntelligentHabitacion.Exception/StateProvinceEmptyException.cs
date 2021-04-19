using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class StateProvinceEmptyException : IntelligentHabitacionException
    {
        public StateProvinceEmptyException() : base(ResourceTextException.STATEPROVINCE_EMPTY)
        {
        }
    }
}
