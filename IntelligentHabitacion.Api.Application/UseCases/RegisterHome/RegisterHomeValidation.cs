using FluentValidation;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;

namespace IntelligentHabitacion.Api.Application.UseCases.RegisterHome
{
    public class RegisterHomeValidation : AbstractValidator<RequestRegisterHomeJson>
    {
        public RegisterHomeValidation()
        {
            RuleFor(x => x.ZipCode).NotEmpty().WithMessage(ResourceTextException.ZIPCODE_EMPTY);
            RuleFor(x => x.Address).NotEmpty().WithMessage(ResourceTextException.ADDRESS_EMPTY);
            RuleFor(x => x.Number).NotEmpty().WithMessage(ResourceTextException.NUMBER_EMPTY);
            RuleFor(x => x.City).NotNull().WithMessage(ResourceTextException.CITY_EMPTY);
            RuleFor(x => x.Country).IsInEnum().WithMessage(ResourceTextException.COUNTRY_EMPTY);
            When(x => x.Country == Communication.Enums.CountryEnum.BRAZIL, () =>
            {
                RuleFor(x => x.StateProvince).NotEmpty().WithMessage(ResourceTextException.STATEPROVINCE_EMPTY);
            });
        }
    }
}
