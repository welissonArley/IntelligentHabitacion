using FluentValidation;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using System.Linq;

namespace IntelligentHabitacion.Api.Application.UseCases.Home.UpdateHomeInformations
{
    public class UpdateHomeInformationValidation : AbstractValidator<RequestUpdateHomeJson>
    {
        public UpdateHomeInformationValidation()
        {
            RuleFor(x => x.ZipCode).NotEmpty().WithMessage(ResourceTextException.ZIPCODE_EMPTY);
            RuleFor(x => x.Address).NotEmpty().WithMessage(ResourceTextException.ADDRESS_EMPTY);
            RuleFor(x => x.Number).NotEmpty().WithMessage(ResourceTextException.NUMBER_EMPTY);
            RuleFor(x => x.City).NotNull().WithMessage(ResourceTextException.CITY_EMPTY);
            When(x => x.Rooms.Count > 0, () =>
            {
                RuleFor(x => x.Rooms).Must(x => x.ToList().Distinct().Count() == x.Count()).WithMessage(ResourceTextException.THERE_ARE_DUPLICATED_ROOMS);
            });
        }
    }
}
