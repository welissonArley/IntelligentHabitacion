using IntelligentHabitacion.Communication.Request;

namespace IntelligentHabitacion.Api.Application.UseCases.ChangePassword
{
    public interface IChangePasswordUseCase
    {
        ResponseOutput Execute(RequestChangePasswordJson changePasswordJson);
    }
}
