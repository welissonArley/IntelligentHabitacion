using FluentValidation;
using IntelligentHabitacion.Api.Application.SharedValidators;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using System.Linq;

namespace IntelligentHabitacion.Api.Application.UseCases.User.UpdateUserInformations
{
    public class UpdateUserInformationsValidation : AbstractValidator<RequestUpdateUserJson>
    {
        public UpdateUserInformationsValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ResourceTextException.NAME_EMPTY);
            RuleFor(x => x.Email).NotEmpty().WithMessage(ResourceTextException.EMAIL_EMPTY);
            RuleFor(x => x.Phonenumbers).Must(c => c.Count > 0).WithMessage(ResourceTextException.PHONENUMBER_EMPTY);
            RuleFor(x => x.Phonenumbers).Must(c => c.Count <= 2).WithMessage(ResourceTextException.PHONENUMBER_MAX_TWO);
            RuleFor(x => x.Phonenumbers).Must(c => c.Count() == c.Distinct().Count()).WithMessage(ResourceTextException.PHONENUMBERS_ARE_SAME);
            RuleFor(x => x.EmergencyContacts).Must(c => c.Count > 0).WithMessage(ResourceTextException.EMERGENCYCONTACT_EMPTY);
            RuleFor(x => x.EmergencyContacts).Must(c => c.Count <= 2).WithMessage(ResourceTextException.EMERGENCYCONTACT_MAX_TWO);
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
