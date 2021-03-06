using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.User.UserInformations
{
    public interface IUserInformationsUseCase
    {
        Task<ResponseOutput> Execute();
    }
}
