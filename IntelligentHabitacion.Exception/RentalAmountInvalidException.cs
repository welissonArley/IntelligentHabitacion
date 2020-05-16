using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class RentalAmountInvalidException : IntelligentHabitacionException
    {
        public RentalAmountInvalidException() : base(ResourceTextException.RENTAL_AMOUNT_INVALID)
        {
        }
    }
}
