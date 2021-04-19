using IntelligentHabitacion.App.Model;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.User.RegisterUser
{
    public interface IRegisterUserUseCase
    {
        Task Execute(RegisterUserModel userInformations);
    }
}