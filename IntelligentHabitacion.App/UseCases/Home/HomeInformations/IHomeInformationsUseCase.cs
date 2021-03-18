using IntelligentHabitacion.App.Model;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.Home.HomeInformations
{
    public interface IHomeInformationsUseCase
    {
        Task<HomeModel> Execute();
    }
}
