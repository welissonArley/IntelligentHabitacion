using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.User.UpdateUserInformations
{
    public interface IUpdateUserInformationsUseCase
    {
        Task<ResponseOutput> Execute(RequestUpdateUserJson updateUserJson);
    }
}
