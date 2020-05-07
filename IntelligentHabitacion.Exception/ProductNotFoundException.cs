using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Exception
{
#pragma warning disable S3925
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException() : base(ResourceTextException.PRODUCT_NOT_FOUND)
        {
        }
    }
}
