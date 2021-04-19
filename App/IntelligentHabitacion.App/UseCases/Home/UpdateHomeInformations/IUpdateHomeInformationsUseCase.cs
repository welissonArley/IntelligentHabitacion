using IntelligentHabitacion.App.Model;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.Home.UpdateHomeInformations
{
    public interface IUpdateHomeInformationsUseCase
    {
        Task Execute(HomeModel home);
    }
}
