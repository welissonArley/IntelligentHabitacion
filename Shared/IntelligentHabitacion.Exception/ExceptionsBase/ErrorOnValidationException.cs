using System.Collections.Generic;

namespace IntelligentHabitacion.Exception.ExceptionsBase
{
#pragma warning disable S3925
    public class ErrorOnValidationException : IntelligentHabitacionException
    {
        public List<string> ErrorMensages { get; set; }
        public ErrorOnValidationException(List<string> listErrors) : base("")
        {
            ErrorMensages = listErrors;
        }
    }
}
