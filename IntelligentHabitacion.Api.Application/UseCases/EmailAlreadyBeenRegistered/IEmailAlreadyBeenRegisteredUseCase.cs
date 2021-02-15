using IntelligentHabitacion.Communication.Boolean;

namespace IntelligentHabitacion.Api.Application.UseCases.EmailAlreadyBeenRegistered
{
    public interface IEmailAlreadyBeenRegisteredUseCase
    {
        BooleanJson Execute(string email);
    }
}
