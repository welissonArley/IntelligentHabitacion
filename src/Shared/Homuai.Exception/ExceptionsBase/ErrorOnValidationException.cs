using System.Collections.Generic;

namespace Homuai.Exception.ExceptionsBase
{
    public class ErrorOnValidationException : HomuaiException
    {
        public List<string> ErrorMensages { get; set; }
        public ErrorOnValidationException(List<string> listErrors) : base("")
        {
            ErrorMensages = listErrors;
        }
    }
}
