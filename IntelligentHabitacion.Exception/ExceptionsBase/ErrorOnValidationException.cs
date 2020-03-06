using System.Collections.Generic;

namespace IntelligentHabitacion.Exception.ExceptionsBase
{
    public class ErrorOnValidationException : IntelligentHabitacionException
    {
        public List<string> ErrorMensages { get; set; }
        public ErrorOnValidationException(List<string> listErrors) : base("")
        {
            ErrorMensages = listErrors;
        }
    }
}
