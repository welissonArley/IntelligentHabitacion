using IntelligentHabitacion.Communication.Request;

namespace IntelligentHabitacion.Api.Application.UseCases.RegisterUser
{
    public interface IRegisterUserUseCase
    {
        string Execute(RequestRegisterUserJson registerUserJson);
    }
}
