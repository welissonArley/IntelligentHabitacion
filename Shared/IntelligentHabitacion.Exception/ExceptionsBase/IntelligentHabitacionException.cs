using System;

namespace IntelligentHabitacion.Exception.ExceptionsBase
{
#pragma warning disable S3925
    public class IntelligentHabitacionException : SystemException
    {
        public IntelligentHabitacionException(string mensagem) : base(mensagem)
        {
        }
    }
}
