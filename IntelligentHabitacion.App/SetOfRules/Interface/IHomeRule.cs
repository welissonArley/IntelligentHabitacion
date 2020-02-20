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
    }
}
