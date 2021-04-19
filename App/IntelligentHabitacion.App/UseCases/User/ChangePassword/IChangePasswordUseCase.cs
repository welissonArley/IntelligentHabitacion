using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.User.ChangePassword
{
    public interface IChangePasswordUseCase
    {
        Task Execute(string currentPassword, string newPassword);
    }
}
