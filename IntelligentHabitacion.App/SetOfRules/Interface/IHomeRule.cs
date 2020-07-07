using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.Communication.Response;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Interface
{
    public interface IHomeRule
    {
        Task<ResponseLocationJson> ValidadeZipCode(string zipCode);
        void ValidadeCity(string city);
        void ValidadeAdress(string address);
        void ValidadeNumber(string number);
        void ValidadeNeighborhood(string neighborhood);
        void ValidadeNetWorkInformation(string name, string password);
        Task Create(HomeModel model);
        Task<HomeModel> GetInformations();
        Task UpdateInformations(HomeModel model);
        Task Delete(string code, string password);
        Task RequestCodeDeleteHome();
    }
}
