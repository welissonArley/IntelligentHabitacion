using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception.Parameters
{
    public class ParametersEmptyOrNullException : IntelligentHabitacionException
    {
        public ParametersEmptyOrNullException() : base(ResourceTextException.PARAMETERS_NULL_OR_EMPTY)
        {

        }
    }
}
