using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class AmountProductsInvalidException : IntelligentHabitacionException
    {
        public AmountProductsInvalidException() : base(ResourceTextException.AMOUNT_PRODUCTS_INVALID)
        {
        }
    }
}
