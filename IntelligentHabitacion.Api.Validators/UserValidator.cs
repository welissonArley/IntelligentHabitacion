using FluentValidation;
using FluentValidation.Validators;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Validators.Validator;
using System.Collections.Generic;

namespace IntelligentHabitacion.Api.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ResourceTextException.NAME_EMPTY);
            RuleFor(x => x.Email).NotEmpty().WithMessage(ResourceTextException.EMAIL_EMPTY);
            When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
            {
                RuleFor(x => x.Email).EmailAddress().WithMessage(ResourceTextException.EMAIL_INVALID);
            });
            RuleFor(x => x.Phonenumbers).Must(c => c.Count > 0).WithMessage(ResourceTextException.PHONENUMBER_EMPTY);
            When(x => x.Phonenumbers.Count > 0, () =>
            {
                RuleFor(x => x.Phonenumbers).Custom((phoneNumbers, contexto) =>
                {
                    var phoneNumberValidator = new PhoneNumberValidator();
                    foreach (var phonenumber in phoneNumbers)
                    {
                        try
                        {
                            phoneNumberValidator.IsValid(phonenumber.Number);
                        }
                        catch
                        {
                            contexto.AddFailure(string.Format(ResourceTextException.THE_PHONENUMBER_INVALID, phonenumber.Number));
                        }
                    }
                });
            });
            RuleFor(x => x.EmergecyContacts).Must(c => c.Count > 0).WithMessage(ResourceTextException.PHONENUMBER_EMPTY);
            When(x => x.EmergecyContacts.Count > 0, () =>
            {
                RuleFor(x => x.EmergecyContacts).Custom((emergecyContacts, context) =>
                {
                    ValidateEmergecyContact(emergecyContacts, context);
                });
            });
        }

        private void ValidateEmergecyContact(ICollection<EmergencyContact> emergecyContacts, CustomContext context)
        {
            var index = 1;
            var phoneNumberValidator = new PhoneNumberValidator();

            foreach (var emergecyContact in emergecyContacts)
            {
                if (string.IsNullOrWhiteSpace(emergecyContact.Name))
                    context.AddFailure(string.Format(ResourceTextException.THE_NAME_EMERGENCY_CONTACT_INVALID, index));

                if (string.IsNullOrWhiteSpace(emergecyContact.DegreeOfKinship))
                    context.AddFailure(string.Format(ResourceTextException.THE_FAMILYRELATIONSHIP_EMERGENCY_CONTACT_INVALID, index));

                if (emergecyContact.Phonenumbers.Count == 0)
                    context.AddFailure(string.Format(ResourceTextException.PHONENUMBER_EMERGENCY_CONTACT_EMPTY, index));
                else
                {
                    foreach (var phonenumber in emergecyContact.Phonenumbers)
                    {
                        try
                        {
                            phoneNumberValidator.IsValid(phonenumber.Number);
                        }
                        catch
                        {
                            context.AddFailure(string.Format(ResourceTextException.PHONENUMBER_EMERGENCY_CONTACT_INVALID, index));
                        }
                    }
                }

                index++;
            }
        }
    }
}
