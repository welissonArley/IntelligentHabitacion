using IntelligentHabitacion.Communication.Boolean;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.EmailAlreadyBeenRegistered
{
    public interface IEmailAlreadyBeenRegisteredUseCase
    {
        Task<BooleanJson> Execute(string email);
    }
}
