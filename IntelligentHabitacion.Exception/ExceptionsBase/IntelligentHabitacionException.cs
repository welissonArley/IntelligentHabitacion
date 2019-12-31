using System;

namespace IntelligentHabitacion.Exception.ExceptionsBase
{
    public class IntelligentHabitacionException : SystemException
    {
        public IntelligentHabitacionException(string mensagem) : base(mensagem)
        {
        }
    }
}
