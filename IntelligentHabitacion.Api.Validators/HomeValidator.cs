using FluentValidation;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using IntelligentHabitacion.Validators.Validator;

namespace IntelligentHabitacion.Api.Validators
{
    public class HomeValidator : AbstractValidator<Home>
    {
        public HomeValidator()
        {
            RuleFor(x => x.City).NotEmpty().WithMessage(ResourceTextException.CITY_EMPTY);
            RuleFor(x => x.State).NotEmpty().WithMessage(ResourceTextException.CITY_EMPTY);
            RuleFor(x => x.Country).NotEmpty().WithMessage(ResourceTextException.COUNTRY_EMPTY);
            RuleFor(x => x.CountryAbbreviation).NotEmpty().WithMessage(ResourceTextException.COUNTRY_ABBREVIATION_EMPTY);
            RuleFor(x => x).Custom((home, context) =>
            {
                if ((string.IsNullOrWhiteSpace(home.NetworksName) && !string.IsNullOrWhiteSpace(home.NetworksPassword)) || (!string.IsNullOrWhiteSpace(home.NetworksName) && string.IsNullOrWhiteSpace(home.NetworksPassword)))
                    context.AddFailure(ResourceTextException.NETWORKINFORMATIONS_INVALID);
            });
            RuleFor(x => x.Address).NotEmpty().WithMessage(ResourceTextException.ADDRESS_EMPTY);
            RuleFor(x => x.Number).NotEmpty().WithMessage(ResourceTextException.NUMBER_EMPTY);
            RuleFor(x => x.Neighborhood).NotEmpty().WithMessage(ResourceTextException.NEIGHBORHOOD_EMPTY);
            RuleFor(x => x.ZipCode).Custom((zipcode, context) =>
            {
                try
                {
                    new ZipCodeValidator().IsValid(zipcode);
                }
                catch (IntelligentHabitacionException ex)
                {
                    context.AddFailure(ex.Message);
                }
            });
        }
    }
}
