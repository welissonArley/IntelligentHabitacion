using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class NetworkInformationsInvalidException : IntelligentHabitacionException
    {
        public NetworkInformationsInvalidException() : base(ResourceTextException.NETWORKINFORMATIONS_INVALID)
        {
        }
    }
}
