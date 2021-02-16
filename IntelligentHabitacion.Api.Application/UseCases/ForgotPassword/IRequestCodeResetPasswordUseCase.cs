namespace IntelligentHabitacion.Api.Application.UseCases.ForgotPassword
{
    public interface IRequestCodeResetPasswordUseCase
    {
        void Execute(string email);
    }
}
