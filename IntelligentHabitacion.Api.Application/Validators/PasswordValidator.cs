using FluentValidation.Validators;
using IntelligentHabitacion.Exception;

namespace IntelligentHabitacion.Api.Application.Validators
{
    public class PasswordValidator
    {
        public void IsValid(string password, CustomContext context)
        {
            if (string.IsNullOrWhiteSpace(password))
                context.AddFailure(ResourceTextException.PASSWORD_EMPTY);
            else if (password.Length < 6)
                context.AddFailure(ResourceTextException.INVALID_PASSWORD);
        }
    }
}
