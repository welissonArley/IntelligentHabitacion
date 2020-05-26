using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class QuantityProductsInvalidException : IntelligentHabitacionException
    {
        public QuantityProductsInvalidException() : base(ResourceTextException.QUANTITY_PRODUCTS_INVALID)
        {
        }
    }
}
