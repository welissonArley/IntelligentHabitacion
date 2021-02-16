using IntelligentHabitacion.Communication.Request;

namespace IntelligentHabitacion.Api.Application.UseCases.Login
{
    public interface ILoginUseCase
    {
        ResponseOutput Execute(RequestLoginJson loginJson);
    }
}
