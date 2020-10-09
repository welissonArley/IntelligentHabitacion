﻿using FluentValidation;
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
            RuleFor(x => x.Country).IsInEnum().WithMessage(ResourceTextException.COUNTRY_EMPTY);
            RuleFor(x => x).Custom((home, context) =>
            {
                if ((string.IsNullOrWhiteSpace(home.NetworksName) && !string.IsNullOrWhiteSpace(home.NetworksPassword)) || (!string.IsNullOrWhiteSpace(home.NetworksName) && string.IsNullOrWhiteSpace(home.NetworksPassword)))
                    context.AddFailure(ResourceTextException.NETWORKINFORMATIONS_INVALID);
            });
            RuleFor(x => x.Address).NotEmpty().WithMessage(ResourceTextException.ADDRESS_EMPTY);
            RuleFor(x => x.Number).NotEmpty().WithMessage(ResourceTextException.NUMBER_EMPTY);
            //TODO: alterar a forma de executar este validator para considerar pais, verificando assim, cep, estado, etc
            /*RuleFor(x => x.ZipCode).Custom((zipcode, context) =>
            {
                try
                {
                    new ZipCodeValidator().IsValid(zipcode);
                }
                catch (IntelligentHabitacionException ex)
                {
                    context.AddFailure(ex.Message);
                }
            });*/
            RuleFor(x => x.DeadlinePaymentRent).Must(c => c >= 1 && c <= 28).WithMessage(ResourceTextException.DEADLINE_FOR_PAYMENT_OF_RENT_INVALID);
        }
    }
}
