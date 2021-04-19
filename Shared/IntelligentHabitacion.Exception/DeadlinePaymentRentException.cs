using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class DeadlinePaymentRentException : IntelligentHabitacionException
    {
        public DeadlinePaymentRentException() : base(ResourceTextException.DEADLINE_FOR_PAYMENT_OF_RENT_INVALID)
        {
        }
    }
}
