using FluentValidation;
using IntelligentHabitacion.Api.Application.Services.Cryptography;
using IntelligentHabitacion.Api.Application.SharedValidators;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;

namespace IntelligentHabitacion.Api.Application.UseCases.ChangePassword
{
    public class ChangePasswordValidation : AbstractValidator<RequestChangePasswordJson>
    {
        public ChangePasswordValidation(PasswordEncripter passwordEncripter, Domain.Entity.User userDataNow)
        {
            RuleFor(x => x.CurrentPassword).Must(c => userDataNow.Password.Equals(passwordEncripter.Encrypt(c))).WithMessage(ResourceTextException.CURRENT_PASSWORD_INVALID);
            RuleFor(x => x.NewPassword).Custom((password, context) =>
            {
                new PasswordValidator().IsValid(password, context);
            });
        }
    }
}
