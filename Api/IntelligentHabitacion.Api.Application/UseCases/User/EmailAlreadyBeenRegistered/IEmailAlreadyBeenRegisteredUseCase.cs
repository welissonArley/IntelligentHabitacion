using IntelligentHabitacion.Communication.Boolean;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.User.EmailAlreadyBeenRegistered
{
    public interface IEmailAlreadyBeenRegisteredUseCase
    {
        Task<BooleanJson> Execute(string email);
    }
}
