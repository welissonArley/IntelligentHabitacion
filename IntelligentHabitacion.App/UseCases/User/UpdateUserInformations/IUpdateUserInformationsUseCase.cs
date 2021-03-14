using IntelligentHabitacion.App.Model;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.User.UpdateUserInformations
{
    public interface IUpdateUserInformationsUseCase
    {
        Task Execute(UserInformationsModel userInformations);
    }
}
