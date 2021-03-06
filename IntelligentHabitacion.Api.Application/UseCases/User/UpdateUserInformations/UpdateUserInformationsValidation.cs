using FluentValidation;
using IntelligentHabitacion.Api.Application.SharedValidators;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;

namespace IntelligentHabitacion.Api.Application.UseCases.User.UpdateUserInformations
{
    public class UpdateUserInformationsValidation : AbstractValidator<RequestUpdateUserJson>
    {
        public UpdateUserInformationsValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ResourceTextException.NAME_EMPTY);
            RuleFor(x => x.Email).NotEmpty().WithMessage(ResourceTextException.EMAIL_EMPTY);
            RuleFor(x => x.Phonenumbers).Must(c => c.Count > 0).WithMessage(ResourceTextException.PHONENUMBER_EMPTY);
            RuleFor(x => x.EmergencyContacts).Must(c => c.Count > 0).WithMessage(ResourceTextException.PHONENUMBER_EMPTY);
            When(x => x.EmergencyContacts.Count > 0, () =>
            {
                RuleFor(x => x.EmergencyContacts).Custom((emergecyContacts, context) =>
                {
                    new EmergencyContactsValidator().IsValid(emergecyContacts, context);
                });
            });
        }
    }
}
