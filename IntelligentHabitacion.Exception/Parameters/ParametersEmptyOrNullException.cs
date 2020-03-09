using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception.Parameters
{
#pragma warning disable S3925
    public class ParametersEmptyOrNullException : IntelligentHabitacionException
    {
        public ParametersEmptyOrNullException() : base(ResourceTextException.PARAMETERS_NULL_OR_EMPTY)
        {

        }
    }
}
