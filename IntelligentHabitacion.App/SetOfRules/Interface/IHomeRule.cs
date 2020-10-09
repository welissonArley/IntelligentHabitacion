using IntelligentHabitacion.App.Model;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Interface
{
    public interface IHomeRule
    {
        Task Create(HomeModel model);
        Task UpdateInformations(HomeModel model);
        Task Delete(string code, string password);
        Task RequestCodeDeleteHome();
        Task<HomeModel> GetInformations();
    }
}
