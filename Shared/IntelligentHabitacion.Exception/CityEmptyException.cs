using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class CityEmptyException : IntelligentHabitacionException
    {
        public CityEmptyException() : base(ResourceTextException.CITY_EMPTY)
        {
        }
    }
}
