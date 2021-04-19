using IntelligentHabitacion.App.Model;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.Home.RegisterHome.Brazil
{
    public interface IRequestCEPUseCase
    {
        Task<HomeModel> Execute(string cep);
    }
}
