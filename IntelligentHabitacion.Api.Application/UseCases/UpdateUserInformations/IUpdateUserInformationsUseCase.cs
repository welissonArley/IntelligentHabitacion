using IntelligentHabitacion.Communication.Request;

namespace IntelligentHabitacion.Api.Application.UseCases.UpdateUserInformations
{
    public interface IUpdateUserInformationsUseCase
    {
        ResponseOutput Execute(RequestUpdateUserJson updateUserJson);
    }
}
