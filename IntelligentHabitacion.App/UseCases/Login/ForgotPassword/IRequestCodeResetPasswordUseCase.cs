using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.Login.ForgotPassword
{
    public interface IRequestCodeResetPasswordUseCase
    {
        Task Execute(string email);
    }
}
