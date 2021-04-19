using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.Login.ForgotPassword
{
    public interface IRequestCodeResetPasswordUseCase
    {
        Task Execute(string email);
    }
}
