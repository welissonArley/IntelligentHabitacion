using IntelligentHabitacion.Communication.Request;

namespace IntelligentHabitacion.Api.Application.UseCases.RegisterUser
{
    public interface IRegisterUserUseCase
    {
        ResponseOutput Execute(RequestRegisterUserJson registerUserJson);
    }
}
