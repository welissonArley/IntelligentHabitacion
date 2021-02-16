using IntelligentHabitacion.Communication.Request;

namespace IntelligentHabitacion.Api.Application.UseCases.ForgotPassword
{
    public interface IResetPasswordUseCase
    {
        void Execute(RequestResetYourPasswordJson resetYourPasswordJson);
    }
}
