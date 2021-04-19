using FluentValidation;
using IntelligentHabitacion.Api.Application.SharedValidators;
using IntelligentHabitacion.Api.Application.UseCases.User.EmailAlreadyBeenRegistered;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;

namespace IntelligentHabitacion.Api.Application.UseCases.User.RegisterUser
{
    public class RegisterUserValidation : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidation(IEmailAlreadyBeenRegisteredUseCase registeredUseCase)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ResourceTextException.NAME_EMPTY);
            RuleFor(x => x.Email).NotEmpty().WithMessage(ResourceTextException.EMAIL_EMPTY);
            RuleFor(x => x.PushNotificationId).NotEmpty().WithMessage(ResourceTextException.PUSHNOTIFICATION_INVALID);
            When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
            {
                RuleFor(x => x.Email).EmailAddress().WithMessage(ResourceTextException.EMAIL_INVALID);
                RuleFor(x => x.Email).MustAsync(async (email, cancelation) =>
                {
                    var exists = await registeredUseCase.Execute(email);

                    return !exists.Value;

                }).WithMessage(ResourceTextException.EMAIL_ALREADYBEENREGISTERED);
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
                    new EmergencyContactsValidator().IsValid(emergecyContacts, context);
                });
            });
        }
    }
}
