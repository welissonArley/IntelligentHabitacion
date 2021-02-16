using FluentValidation;
using FluentValidation.Validators;
using IntelligentHabitacion.Api.Application.SharedValidators;
using IntelligentHabitacion.Api.Domain.Repository.Code;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using System;

namespace IntelligentHabitacion.Api.Application.UseCases.ForgotPassword
{
    public class ForgotPasswordValidation : AbstractValidator<RequestResetYourPasswordJson>
    {
        public ForgotPasswordValidation(ICodeReadOnlyRepository codeRepository, IUserReadOnlyRepository userReadOnlyRepository)
        {
            RuleFor(x => x).Custom((request, context) =>
            {
                var user = userReadOnlyRepository.GetByEmail(request.Email);

                if (user == null)
                    context.AddFailure(ResourceTextException.INVALID_USER);
                else
                {
                    var code = codeRepository.GetByUserId(user.Id);
                    if (code == null)
                        context.AddFailure(ResourceTextException.CODE_RESET_PASSWORD_REQUIRED);
                    else
                        ValidateCode(code, request.Code, context);
                }
            });
            RuleFor(x => x.Password).Custom((password, context) =>
            {
                new PasswordValidator().IsValid(password, context);
            });
        }

        private void ValidateCode(Domain.Entity.Code code, string codeReceived, CustomContext context)
        {
            if (!code.Value.Equals(codeReceived.ToUpper()))
                context.AddFailure(ResourceTextException.CODE_INVALID);

            if (DateTime.Compare(code.CreateDate.AddHours(1), DateTime.UtcNow) <= 0)
                context.AddFailure(ResourceTextException.EXPIRED_CODE);
        }
    }
}
