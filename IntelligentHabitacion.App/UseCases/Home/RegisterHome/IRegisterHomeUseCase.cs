using IntelligentHabitacion.App.Model;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.Home.RegisterHome
{
    public interface IRegisterHomeUseCase
    {
        Task Execute(HomeModel home);
    }
}
