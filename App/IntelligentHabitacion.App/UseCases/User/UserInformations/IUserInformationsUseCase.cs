using IntelligentHabitacion.App.Model;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.User.UserInformations
{
    public interface IUserInformationsUseCase
    {
        Task<UserInformationsModel> Execute();
    }
}
