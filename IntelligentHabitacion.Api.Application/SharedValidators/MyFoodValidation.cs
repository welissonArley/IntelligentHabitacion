using FluentValidation;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using System;

namespace IntelligentHabitacion.Api.Application.SharedValidators
{
    public class MyFoodValidation : AbstractValidator<RequestProductJson>
    {
        public MyFoodValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ResourceTextException.PRODUCT_NAME_EMPTY);
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage(ResourceTextException.QUANTITY_PRODUCTS_INVALID);
            RuleFor(x => x.Type).IsInEnum().WithMessage(ResourceTextException.TYPE_PRODUCTS_INVALID);
            When(x => x.DueDate.HasValue, () =>
            {
                RuleFor(x => x.DueDate).Must(c => DateTime.Compare(c.Value.Date, DateTime.UtcNow.Date) > 0).WithMessage(ResourceTextException.DUEDATE_MUST_BE_GRATER_THAN_TODAY);
            });
        }
    }
}
