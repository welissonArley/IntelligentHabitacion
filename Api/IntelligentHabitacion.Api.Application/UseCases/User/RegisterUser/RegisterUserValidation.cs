using FluentValidation;
using IntelligentHabitacion.Api.Application.SharedValidators;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using System.Linq;

namespace IntelligentHabitacion.Api.Application.UseCases.User.RegisterUser
{
    public class RegisterUserValidation : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidation(IUserReadOnlyRepository repository)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ResourceTextException.NAME_EMPTY);
            RuleFor(x => x.Email).NotEmpty().WithMessage(ResourceTextException.EMAIL_EMPTY);
            RuleFor(x => x.PushNotificationId).NotEmpty().WithMessage(ResourceTextException.PUSHNOTIFICATION_INVALID);
            When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
            {
                RuleFor(x => x.Email).EmailAddress().WithMessage(ResourceTextException.EMAIL_INVALID);
                RuleFor(x => x.Email).MustAsync(async (email, cancelation) =>
                {
                    var exists = await repository.ExistActiveUserWithEmail(email);

                    return !exists;

                }).WithMessage(ResourceTextException.EMAIL_ALREADYBEENREGISTERED);
            });
            RuleFor(x => x.Password).Custom((password, context) =>
            {
                new PasswordValidator().IsValid(password, context);
            });
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
