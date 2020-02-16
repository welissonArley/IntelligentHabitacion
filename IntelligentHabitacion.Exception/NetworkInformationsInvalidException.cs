using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
    public class NetworkInformationsInvalidException : IntelligentHabitacionException
    {
        public NetworkInformationsInvalidException() : base(ResourceTextException.NETWORKINFORMATIONS_INVALID)
        {
        }
    }
}
