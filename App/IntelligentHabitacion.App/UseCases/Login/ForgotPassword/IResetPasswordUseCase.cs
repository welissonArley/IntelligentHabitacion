using IntelligentHabitacion.App.Model;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.Login.ForgotPassword
{
    public interface IResetPasswordUseCase
    {
        Task Execute(ForgetPasswordModel model);
    }
}
