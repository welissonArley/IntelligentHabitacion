using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.UserInformations
{
    public interface IUserInformationsUseCase
    {
        Task<ResponseOutput> Execute();
    }
}
