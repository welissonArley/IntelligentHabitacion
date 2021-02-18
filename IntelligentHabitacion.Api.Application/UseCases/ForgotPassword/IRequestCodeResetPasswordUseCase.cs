using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.ForgotPassword
{
    public interface IRequestCodeResetPasswordUseCase
    {
        Task Execute(string email);
    }
}
