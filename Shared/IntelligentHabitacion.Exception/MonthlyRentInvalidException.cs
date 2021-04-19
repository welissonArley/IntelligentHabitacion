using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class MonthlyRentInvalidException : IntelligentHabitacionException
    {
        public MonthlyRentInvalidException() : base(ResourceTextException.MONTHLYRENT_INVALID)
        {
        }
    }
}
