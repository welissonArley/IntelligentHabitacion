using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
    public class CityEmptyException : IntelligentHabitacionException
    {
        public CityEmptyException() : base(ResourceTextException.CITY_EMPTY)
        {
        }
    }
}
