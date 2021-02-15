using FluentValidation;
using FluentValidation.Validators;
using IntelligentHabitacion.Api.Application.Validators;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using System.Collections.Generic;

namespace IntelligentHabitacion.Api.Application.UseCases.RegisterUser
{
    public class RegisterUserValidation : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ResourceTextException.NAME_EMPTY);
            RuleFor(x => x.Email).NotEmpty().WithMessage(ResourceTextException.EMAIL_EMPTY);
            RuleFor(x => x.PushNotificationId).NotEmpty().WithMessage(ResourceTextException.PUSHNOTIFICATION_INVALID);
            When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
            {
                RuleFor(x => x.Email).EmailAddress().WithMessage(ResourceTextException.EMAIL_INVALID);
            });
            RuleFor(x => x.Password).Custom((password, context) =>
            {
                new PasswordValidator().IsValid(password, context);
            });
            RuleFor(x => x.Phonenumbers).Must(c => c.Count > 0).WithMessage(ResourceTextException.PHONENUMBER_EMPTY);
            RuleFor(x => x.EmergencyContacts).Must(c => c.Count > 0).WithMessage(ResourceTextException.PHONENUMBER_EMPTY);
            When(x => x.EmergencyContacts.Count > 0, () =>
            {
                RuleFor(x => x.EmergencyContacts).Custom((emergecyContacts, context) =>
                {
                    ValidateEmergecyContact(emergecyContacts, context);
                });
            });
        }

        private void ValidateEmergecyContact(ICollection<RequestEmergencyContactJson> emergecyContacts, CustomContext context)
        {
            var index = 1;

            foreach (var emergecyContact in emergecyContacts)
            {
                if (string.IsNullOrWhiteSpace(emergecyContact.Name))
                    context.AddFailure(string.Format(ResourceTextException.THE_NAME_EMERGENCY_CONTACT_INVALID, index));

                if (string.IsNullOrWhiteSpace(emergecyContact.Relationship))
                    context.AddFailure(string.Format(ResourceTextException.THE_RELATIONSHIP_EMERGENCY_CONTACT_INVALID, index));

                if (string.IsNullOrWhiteSpace(emergecyContact.Phonenumber))
                    context.AddFailure(string.Format(ResourceTextException.PHONENUMBER_EMERGENCY_CONTACT_EMPTY, index));

                index++;
            }
        }
    }
}
