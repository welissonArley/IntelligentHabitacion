using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.User.EmailAlreadyBeenRegistered
{
    public interface IEmailAlreadyBeenRegisteredUseCase
    {
        Task Execute(string email);
    }
}
