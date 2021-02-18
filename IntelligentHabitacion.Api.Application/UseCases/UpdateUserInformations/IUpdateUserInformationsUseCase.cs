using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.UpdateUserInformations
{
    public interface IUpdateUserInformationsUseCase
    {
        Task<ResponseOutput> Execute(RequestUpdateUserJson updateUserJson);
    }
}
