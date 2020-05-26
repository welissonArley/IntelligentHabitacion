using FluentValidation;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Exception;

namespace IntelligentHabitacion.Api.Validators
{
    public class MyFoodValidator : AbstractValidator<MyFood>
    {
        public MyFoodValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ResourceTextException.PRODUCT_NAME_EMPTY);
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage(ResourceTextException.QUANTITY_PRODUCTS_INVALID);
            RuleFor(x => x.Type).IsInEnum().WithMessage(ResourceTextException.TYPE_PRODUCTS_INVALID);
        }
    }
}
