using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class ProductNameEmptyException : IntelligentHabitacionException
    {
        public ProductNameEmptyException() : base(ResourceTextException.PRODUCT_NAME_EMPTY)
        {
        }
    }
}
